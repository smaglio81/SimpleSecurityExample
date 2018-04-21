SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating role SimpleSecurityRole'
GO
CREATE ROLE [SimpleSecurityRole]
AUTHORIZATION [dbo]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
BEGIN TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[tbl_SimpleSecurityAccess]'
GO
CREATE TABLE [dbo].[tbl_SimpleSecurityAccess]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[UcsbNetId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[UcsbCampusId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Allowed] [bit] NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_tbl_SimpleSecurityAccess] on [dbo].[tbl_SimpleSecurityAccess]'
GO
ALTER TABLE [dbo].[tbl_SimpleSecurityAccess] ADD CONSTRAINT [PK_tbl_SimpleSecurityAccess] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[tbl_SimpleSecurityAccessLog]'
GO
CREATE TABLE [dbo].[tbl_SimpleSecurityAccessLog]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[DateTime] [datetime] NOT NULL CONSTRAINT [DF_tbl_SimpleSecurityAuditLog_DateTime] DEFAULT (getdate()),
[UcsbNetId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[UcsbCampusId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Success] [bit] NOT NULL,
[IpAddress] [varchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[XForwardedFor] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ComputerName] [varchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Uri] [varchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Reason] [varchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ErrorMessage] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ErrorStackTrace] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_tbl_SimpleSecurityAuditLog] on [dbo].[tbl_SimpleSecurityAccessLog]'
GO
ALTER TABLE [dbo].[tbl_SimpleSecurityAccessLog] ADD CONSTRAINT [PK_tbl_SimpleSecurityAuditLog] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_Id_DateTime_UcsbCampusId_Success_IpAddress_ComputerName] on [dbo].[tbl_SimpleSecurityAccessLog]'
GO
CREATE NONCLUSTERED INDEX [IX_Id_DateTime_UcsbCampusId_Success_IpAddress_ComputerName] ON [dbo].[tbl_SimpleSecurityAccessLog] ([Id], [DateTime] DESC, [UcsbCampusId], [Success], [IpAddress], [ComputerName])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_Id_DateTime_UcsbCampusId_Success_XForwardedFor_ComputerName] on [dbo].[tbl_SimpleSecurityAccessLog]'
GO
CREATE NONCLUSTERED INDEX [IX_Id_DateTime_UcsbCampusId_Success_XForwardedFor_ComputerName] ON [dbo].[tbl_SimpleSecurityAccessLog] ([Id], [DateTime] DESC, [UcsbCampusId], [Success], [XForwardedFor], [ComputerName])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_Id_DateTime_UcsbNetId_Success_IpAddress_ComputerName] on [dbo].[tbl_SimpleSecurityAccessLog]'
GO
CREATE NONCLUSTERED INDEX [IX_Id_DateTime_UcsbNetId_Success_IpAddress_ComputerName] ON [dbo].[tbl_SimpleSecurityAccessLog] ([Id], [DateTime] DESC, [UcsbNetId], [Success], [IpAddress], [ComputerName])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_Id_DateTime_UcsbNetId_Success_XForwardedFor_ComputerName] on [dbo].[tbl_SimpleSecurityAccessLog]'
GO
CREATE NONCLUSTERED INDEX [IX_Id_DateTime_UcsbNetId_Success_XForwardedFor_ComputerName] ON [dbo].[tbl_SimpleSecurityAccessLog] ([Id], [DateTime] DESC, [UcsbNetId], [Success], [XForwardedFor], [ComputerName])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Altering permissions on  [dbo].[tbl_SimpleSecurityAccessLog]'
GO
GRANT SELECT ON  [dbo].[tbl_SimpleSecurityAccessLog] TO [SimpleSecurityRole]
GO
GRANT INSERT ON  [dbo].[tbl_SimpleSecurityAccessLog] TO [SimpleSecurityRole]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Altering permissions on  [dbo].[tbl_SimpleSecurityAccess]'
GO
GRANT SELECT ON  [dbo].[tbl_SimpleSecurityAccess] TO [SimpleSecurityRole]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
COMMIT TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
DECLARE @Success AS BIT
SET @Success = 1
SET NOEXEC OFF
IF (@Success = 1) PRINT 'The database update succeeded'
ELSE BEGIN
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT 'The database update failed'
END
GO