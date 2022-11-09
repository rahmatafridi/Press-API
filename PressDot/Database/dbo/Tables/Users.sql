CREATE TABLE [dbo].[Users] (
    [UserId]       INT            IDENTITY (1, 1) NOT NULL,
    [Firstname]    NVARCHAR (50)  NOT NULL,
    [Lastname]     NVARCHAR (50)  NOT NULL,
    [Email]        NVARCHAR (200) NOT NULL,
    [MobileNumber] NVARCHAR (20)  NOT NULL,
    [Password]     NVARCHAR (100) NOT NULL,
    [PasswordSalt] NVARCHAR (20)  NOT NULL,
    [UserRoleId]   INT            NOT NULL,
    [IsActive]     BIT            CONSTRAINT [DF_Customer_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedDate]  DATETIME       NOT NULL DEFAULT GETDATE(),
    [CreatedBy]    INT            NULL,
    [UpdatedDate]  DATETIME       NULL,
    [UpdatedBy]    INT            NULL,
    [DeletedDate]  DATETIME       NULL,
    [DeletedBy]    INT            NULL,
    [IsDeleted]    BIT            CONSTRAINT [DF_Customer_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_Users_UserRole] FOREIGN KEY ([UserRoleId]) REFERENCES [dbo].[UserRole] ([UserRoleId]) 
);

