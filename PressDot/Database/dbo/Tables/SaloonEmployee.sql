CREATE TABLE [dbo].[SaloonEmployee](
	[SaloonEmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[SaloonId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_SaloonEmployee] PRIMARY KEY CLUSTERED 
(
	[SaloonEmployeeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SaloonEmployee] ADD  CONSTRAINT [DF_SaloonEmployee_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[SaloonEmployee] ADD  CONSTRAINT [DF_SaloonEmployee_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO


ALTER TABLE [dbo].[SaloonEmployee]  WITH CHECK ADD  CONSTRAINT [FK_SaloonEmployee_Saloon] FOREIGN KEY([SaloonId])
REFERENCES [dbo].[Saloon] ([SaloonId])
GO

ALTER TABLE [dbo].[SaloonEmployee] CHECK CONSTRAINT [FK_SaloonEmployee_Saloon]
GO

ALTER TABLE [dbo].[SaloonEmployee]  WITH CHECK ADD  CONSTRAINT [FK_SaloonEmployee_Users] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[SaloonEmployee] CHECK CONSTRAINT [FK_SaloonEmployee_Users]
GO