CREATE TABLE [dbo].[LaboratoryUsers](
	[LaboratoryUsersId] [int] IDENTITY(1,1) NOT NULL,
	[LaboratoryId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[IsEmailReceiver] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_LaboratoryUsers] PRIMARY KEY CLUSTERED 
(
	[LaboratoryUsersId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[LaboratoryUsers] ADD  CONSTRAINT [DF_LaboratoryUsers_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[LaboratoryUsers] ADD  CONSTRAINT [DF_LaboratoryUsers_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[LaboratoryUsers]  WITH CHECK ADD  CONSTRAINT [FK_LaboratoryUsers_Laboratory] FOREIGN KEY([LaboratoryId])
REFERENCES [dbo].[Laboratory] ([LaboratoryId])
GO

ALTER TABLE [dbo].[LaboratoryUsers] CHECK CONSTRAINT [FK_LaboratoryUsers_Laboratory]
GO

ALTER TABLE [dbo].[LaboratoryUsers]  WITH CHECK ADD  CONSTRAINT [FK_LaboratoryUsers_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[LaboratoryUsers] CHECK CONSTRAINT [FK_LaboratoryUsers_Users]
GO
