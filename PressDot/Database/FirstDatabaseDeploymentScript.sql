/*
After deployment of database, create following roles.

*/

SET IDENTITY_INSERT [dbo].[UserRole] ON 
GO
INSERT INTO dbo.UserRole
(
    UserRoleId,
    UserRoleName,
    CreatedDate,
    CreatedBy,
    UpdatedDate,
    UpdatedBy,
    DeletedDate,
    DeletedBy,
    IsDeleted
)
VALUES
(
    1,
    N'Administrator', -- UserRoleName - nvarchar
    Getdate(), -- CreatedDate - datetime
    NULL, -- CreatedBy - int
    NULL, -- UpdatedDate - datetime
    NULL, -- UpdatedBy - int
    NULL, -- DeletedDate - datetime
    NULL, -- DeletedBy - int
    0 -- IsDeleted - bit
)

GO
INSERT INTO dbo.UserRole
(
    UserRoleId,
    UserRoleName,
    CreatedDate,
    CreatedBy,
    UpdatedDate,
    UpdatedBy,
    DeletedDate,
    DeletedBy,
    IsDeleted
)
VALUES
(
    2,
    N'Customer', -- UserRoleName - nvarchar
    Getdate(), -- CreatedDate - datetime
    NULL, -- CreatedBy - int
    NULL, -- UpdatedDate - datetime
    NULL, -- UpdatedBy - int
    NULL, -- DeletedDate - datetime
    NULL, -- DeletedBy - int
    0 -- IsDeleted - bit
)

GO
INSERT INTO dbo.UserRole
(
    UserRoleId,
    UserRoleName,
    CreatedDate,
    CreatedBy,
    UpdatedDate,
    UpdatedBy,
    DeletedDate,
    DeletedBy,
    IsDeleted
)
VALUES
(
    3,
    N'Active Dentist', -- UserRoleName - nvarchar
    Getdate(), -- CreatedDate - datetime
    NULL, -- CreatedBy - int
    NULL, -- UpdatedDate - datetime
    NULL, -- UpdatedBy - int
    NULL, -- DeletedDate - datetime
    NULL, -- DeletedBy - int
    0 -- IsDeleted - bit
)

GO
INSERT INTO dbo.UserRole
(
    UserRoleId,
    UserRoleName,
    CreatedDate,
    CreatedBy,
    UpdatedDate,
    UpdatedBy,
    DeletedDate,
    DeletedBy,
    IsDeleted
)
VALUES
(
    4,
    N'Passive Dentist', -- UserRoleName - nvarchar
    Getdate(), -- CreatedDate - datetime
    NULL, -- CreatedBy - int
    NULL, -- UpdatedDate - datetime
    NULL, -- UpdatedBy - int
    NULL, -- DeletedDate - datetime
    NULL, -- DeletedBy - int
    0 -- IsDeleted - bit
)

GO
INSERT INTO dbo.UserRole
(
    UserRoleId,
    UserRoleName,
    CreatedDate,
    CreatedBy,
    UpdatedDate,
    UpdatedBy,
    DeletedDate,
    DeletedBy,
    IsDeleted
)
VALUES
(
    5,
    N'Laboratory', -- UserRoleName - nvarchar
    Getdate(), -- CreatedDate - datetime
    NULL, -- CreatedBy - int
    NULL, -- UpdatedDate - datetime
    NULL, -- UpdatedBy - int
    NULL, -- DeletedDate - datetime
    NULL, -- DeletedBy - int
    0 -- IsDeleted - bit
)
GO
INSERT INTO dbo.UserRole
(
    UserRoleId,
    UserRoleName,
    CreatedDate,
    CreatedBy,
    UpdatedDate,
    UpdatedBy,
    DeletedDate,
    DeletedBy,
    IsDeleted
)
VALUES
(
    6,
    N'Saloon Administrator', -- UserRoleName - nvarchar
    Getdate(), -- CreatedDate - datetime
    NULL, -- CreatedBy - int
    NULL, -- UpdatedDate - datetime
    NULL, -- UpdatedBy - int
    NULL, -- DeletedDate - datetime
    NULL, -- DeletedBy - int
    0 -- IsDeleted - bit
)
GO
INSERT INTO dbo.UserRole
(
    UserRoleId,
    UserRoleName,
    CreatedDate,
    CreatedBy,
    UpdatedDate,
    UpdatedBy,
    DeletedDate,
    DeletedBy,
    IsDeleted
)
VALUES
(
    7,
    N'Dentist', -- UserRoleName - nvarchar
    Getdate(), -- CreatedDate - datetime
    NULL, -- CreatedBy - int
    NULL, -- UpdatedDate - datetime
    NULL, -- UpdatedBy - int
    NULL, -- DeletedDate - datetime
    NULL, -- DeletedBy - int
    0 -- IsDeleted - bit
)
GO
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO

GO
SET IDENTITY_INSERT [dbo].[Users] ON
GO
INSERT INTO dbo.Users
(
       [UserId]
      ,[Firstname]
      ,[Lastname]
      ,[Email]
      ,[MobileNumber]
      ,[Password]
      ,[PasswordSalt]
      ,[UserRoleId]
      ,[IsActive]
      ,[CreatedDate]
      ,[IsDeleted]
)
VALUES
(
	1,
	'Pressdot',
	'Administrator',
	'admin@pressdot.com',
	'1234567',
	'l1gUBOK+o8mPouko68ZeBEtxYk00WE52OWY=',-- Password is 12345
	'KqbM4XNv9f',
	1,
	1,
	GETDATE(),
	0
)

GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO

GO
SET IDENTITY_INSERT [dbo].[SaloonType] ON
GO
INSERT INTO dbo.SaloonType
(
       [SaloonTypeId]
      ,[SaloonTypeName]
      ,[CreatedDate]
      ,[IsDeleted]
)
VALUES
(
	1,
	'Passive',
	GETDATE(),
	0
)
GO
INSERT INTO dbo.SaloonType
(
       [SaloonTypeId]
      ,[SaloonTypeName]
      ,[CreatedDate]
      ,[IsDeleted]
)
VALUES
(
	2,
	'Normal',
	GETDATE(),
	0
)

GO
SET IDENTITY_INSERT [dbo].[SaloonType] OFF
GO

insert into DaysOfWeek values('Monday',getdate(),null, null,null,null,null,0)
insert into DaysOfWeek values('Tuesday',getdate(),null, null,null,null,null,0)
insert into DaysOfWeek values('Wednesday',getdate(),null, null,null,null,null,0)
insert into DaysOfWeek values('Thursday',getdate(),null, null,null,null,null,0)
insert into DaysOfWeek values('Friday',getdate(),null, null,null,null,null,0)
insert into DaysOfWeek values('Saturday',getdate(),null, null,null,null,null,0)
insert into DaysOfWeek values('Sunday',getdate(),null, null,null,null,null,0)

INSERT INTO [dbo].[States] ([Value],[StateFor],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[DeletedDate],[DeletedBy],[IsDeleted])
     VALUES('Approved','Appointment',getdate(),NULL,getdate(),NULL,null,null,0),
           ('Pending','Appointment',getdate(),NULL,getdate(),NULL,null,null,0),
		   ('Cancel','Appointment',getdate(),NULL,getdate(),NULL,null,null,0),
		   ('Reject','Appointment',getdate(),NULL,getdate(),NULL,null,null,0),
		   ('Created','Order',getdate(),NULL,getdate(),NULL,null,null,0),
		   ('Sent to Laboratory','Order',getdate(),NULL,getdate(),NULL,null,null,0),
		   ('Sent to Saloon','Order',getdate(),NULL,getdate(),NULL,null,null,0),
		   ('Complete','Order',getdate(),NULL,getdate(),NULL,null,null,0)
GO