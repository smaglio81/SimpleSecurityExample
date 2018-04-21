using System;
using System.Data;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Ucsb.Sa.Enterprise.Communication.UcsbDirectory;

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

	    public override async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
	    {
		    if (base.ValidateRequestInput(context, out var request, out var authInfo)) return;

		    string username = authInfo.Username;
		    string password = authInfo.Password;

		    await AuthenticateAsync(username, password, context, cancellationToken);
	    }

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
		protected override async Task<IPrincipal> AuthenticateAsync(
			string username,
			string password,
            HttpAuthenticationContext context,
			CancellationToken cancellationToken
        )
		{
			return await this._AuthorizationAsync(
				username: username,
				password: password,
				context: context,
				cancellationToken: cancellationToken
			);
		}

    }
}
