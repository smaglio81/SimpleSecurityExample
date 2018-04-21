using System;
using System.Data;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using Ucsb.Sa.Enterprise.Communication.UcsbDirectory;
using HttpAuthenticationContextOrAuthorizationFilterContext = System.Web.Http.Filters.HttpAuthenticationContext;

namespace Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity
{
	/// <summary>
	/// Authentication against the Campus LDAP (should be used against ou=Applications). If it
	/// authenticates successfully, then the sites database is checked to see if the UcsbNetId
	/// is approved to use the services. For each each authentication/authorization attempt
	/// and audit log entry is produced and stored in the database alongside the access tables.
	/// </summary>
    public partial class LdapAuthenticationAttribute : BasicAuthenticationAttribute
    {

		[Inject]
        public SimpleSecurityService SimpleSecurityService { get; set; }

		[Inject]
        public AuthenticationDirectorySessionService AuthenticationDirectorySessionService { get; set; }

		/// <summary>
		/// It was supposed to simply authenticate the username & password. But ...
		///
		/// Authentication against the Campus LDAP (should be used against ou=Applications). If it
		/// authenticates successfully, then the sites database is checked to see if the UcsbNetId
		/// is approved to use the services. For each each authentication/authorization attempt
		/// and audit log entry is produced and stored in the database alongside the access tables.
		/// </summary>
		/// <param name="username">Campus LDAP ucsbNetId in ou=Applications</param>
		/// <param name="password">The password</param>
		/// <param name="context">The request/response context.</param>
		/// <param name="cancellationToken">Async cancellation token.</param>
		/// <returns>A <see cref="GenericPrincipal" /> object when success. Null otherwise.</returns>
		protected async Task<IPrincipal> _AuthorizationAsync(
			string username,
			string password,
			HttpAuthenticationContextOrAuthorizationFilterContext context,
			CancellationToken cancellationToken
        )
		{
			var config = SimpleSecurityConfigManager.GetConfig();	// this will throw an error that will bubble up to the top level.
																	// is that the best approach?

            var request = context.Request;
            AccessLogEntry entry = new AccessLogEntry() { UcsbNetId = username };

	        try
	        {
		        //  parse the request
		        try
		        {
			        entry = request.PopulateAccessLogEntry(entry);
		        }
		        catch (Exception e)
		        {
			        var reason = "Error parsing request to create access log entry.";
			        SetupExceptionLogEntryAndErrorResponse(reason, context, entry, e);
			        return null;
				}


		        //  Validate against Campus LDAP
		        DataTable campusLdapResult;
		        try
		        {
					campusLdapResult = AuthenticationDirectorySessionService.RetrieveAny(
						ucsbNetId: username,
						attributes: new [] {"ucsbCampusId"},
						password: password
			        );
		        }
		        catch (Exception e)
		        {
			        var reason = string.Format(
				        "Campus LDAP authentication failed for '{0}'. This may be hiding lower level exceptions.",
				        username
			        );
			        SetupExceptionLogEntryAndErrorResponse(reason, context, entry, e);
					return null;
				}

		        var row = campusLdapResult.Rows[0];
		        entry.UcsbCampusId = row["ucsbCampusId"].ToString();

		        //  Test they have access through the database
		        AccessInfo accessInfo;
		        try
		        {
			        accessInfo = await SimpleSecurityService.GetAccessInfoAsync(ucsbNetId: username);
		        }
		        catch (Exception e)
		        {
			        var reason = string.Format(
				        "An error occurred while looking up data in {1} for ucsbNetId '{0}'. This may be hiding lower level exceptions.",
				        username,
						config.AccessTable
			        );
					SetupExceptionLogEntryAndErrorResponse(reason, context, entry, e);
			        return null;
				}

		        entry.Success = accessInfo.Allowed;
		        if (!accessInfo.Allowed)
		        {
			        string reason = null;
					
					if (string.IsNullOrWhiteSpace(accessInfo.UcsbCampusId))
			        {
				        reason = string.Format("'{0}' was not found in {1}.", username, config.AccessTable);
					}
			        else
			        {
				        reason = string.Format("'{0}' was not allowed access through the {1}.", username, config.AccessTable);
			        }

			        string externalMessage = string.Format("Access is not allowed for '{0}'.", username);
					SetupExceptionLogEntryAndErrorResponse(reason, externalMessage, context, entry, null);

			        return null;
				}

				//	SUCCESS
		        entry.Reason = "Success";
		        var principal = new GenericPrincipal(new GenericIdentity(username), null);
		        context.Principal = principal;

		        return principal;

	        }
	        catch (Exception e)
	        {
		        var reason = "An unexpected error occurred. See messages/stacktrace for details.";
		        SetupExceptionLogEntryAndErrorResponse(reason, context, entry, e);
				return null;
			}
            finally
            {
				await SimpleSecurityService.LogAccessAttemptAsync(entry);
			}
        }

	    internal void SetupExceptionLogEntryAndErrorResponse(
			string reason,
			HttpAuthenticationContextOrAuthorizationFilterContext context,
			AccessLogEntry entry,
			Exception e
		)
	    {
		    SetupExceptionLogEntryAndErrorResponse(reason, reason, context, entry, e);
	    }

	    internal void SetupExceptionLogEntryAndErrorResponse(
		    string reason,
		    string externalMessage,
			HttpAuthenticationContextOrAuthorizationFilterContext context,
		    AccessLogEntry entry,
		    Exception e
	    )
	    {
		    entry.Reason = reason;

		    if (e != null)
		    {
			    AddExceptionInfo(entry, e);
		    }

		    var request = context.Request;
		    context.ErrorResult = new AuthenticationFailureResult(externalMessage, request);
	    }

		/// <summary>
		/// Add exception and inner exception information to the audit log entry object.
		/// </summary>
		/// <param name="entry">Audit log entry object to add to.</param>
		/// <param name="e">The exception to record.</param>
		internal void AddExceptionInfo(AccessLogEntry entry, Exception e)
        {
            var i = 0;
            var errorMessage = new StringBuilder(100);
            var errorStackTrace = new StringBuilder(100);

            var current = e;
            while (current != null)
            {
                if (errorMessage.Length > 0) errorMessage.AppendLine().AppendLine();
                errorMessage.AppendFormat("[{0}] - {1}: {2}", i, current.GetType().FullName, current.Message);

                if (errorStackTrace.Length > 0) errorStackTrace.AppendLine().AppendLine();
                errorStackTrace.AppendFormat("[{0}] - {1}", i, current.StackTrace);

                i++;
                current = current.InnerException;
            }

            entry.ErrorMessage = errorMessage.ToString();
            entry.ErrorStackTrace = errorStackTrace.ToString();
        }

    }
}
