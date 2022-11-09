CREATE TABLE [dbo].[UserDevices]
(
	[UserDeviceId]		INT IDENTITY (1, 1) NOT NULL ,
	[UserId]			INT NOT NULL,
	[DeviceToken]		NVARCHAR(1000) NOT NULL,
    [DeviceId]          NVARCHAR(200)  NOT NULL,
	[CreatedDate]       DATETIME      NOT NULL DEFAULT GETDATE(),
    [CreatedBy]         INT           NULL,
    [UpdatedDate]       DATETIME      NULL,
    [UpdatedBy]         INT           NULL,
    [DeletedDate]       DATETIME      NULL,
    [DeletedBy]         INT           NULL,
    [IsDeleted]         BIT           CONSTRAINT [DF_UserDevice_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_UserDevicePk] PRIMARY KEY CLUSTERED ([UserDeviceId] ASC), 
    CONSTRAINT [FK_UserDevices_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);
