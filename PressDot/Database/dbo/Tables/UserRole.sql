CREATE TABLE [dbo].[UserRole] (
    [UserRoleId]   INT           IDENTITY (1, 1) NOT NULL,
    [UserRoleName] NVARCHAR (50) NOT NULL,
    [CreatedDate]  DATETIME      NOT NULL DEFAULT GETDATE(),
    [CreatedBy]    INT           NULL,
    [UpdatedDate]  DATETIME      NULL,
    [UpdatedBy]    INT           NULL,
    [DeletedDate]  DATETIME      NULL,
    [DeletedBy]    INT           NULL,
    [IsDeleted]    BIT           CONSTRAINT [DF_UserRole_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([UserRoleId] ASC)
);

