CREATE TABLE [dbo].[SaloonEmployeeSchedule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SaloonEmployeeId] [int] NOT NULL,
	[Day] [int] NOT NULL,
	[StartTime] TIME NOT NULL, 
    [EndTime] TIME NOT NULL, 
	[CreatedDate] [datetime] NULL DEFAULT GETDATE(),
	[UpdatedDate] [datetime] NULL,
	[DeletedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[DeletedBy] [int] NULL,
	[IsDeleted] [bit] NOT NULL DEFAULT 0,
 CONSTRAINT [PK_SaloonEmployeeSchedule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SaloonEmployeeSchedule]  WITH CHECK ADD  CONSTRAINT [FK_SaloonEmployeeSchedule_DaysOfWeek] FOREIGN KEY([Day])
REFERENCES [dbo].[DaysOfWeek] ([Id])
GO

ALTER TABLE [dbo].[SaloonEmployeeSchedule] CHECK CONSTRAINT [FK_SaloonEmployeeSchedule_DaysOfWeek]
GO

ALTER TABLE [dbo].[SaloonEmployeeSchedule]  WITH CHECK ADD  CONSTRAINT [FK_SaloonEmployeeSchedule_SaloonEmployee] FOREIGN KEY([SaloonEmployeeId])
REFERENCES [dbo].[SaloonEmployee] ([SaloonEmployeeId])
GO

ALTER TABLE [dbo].[SaloonEmployeeSchedule] CHECK CONSTRAINT [FK_SaloonEmployeeSchedule_SaloonEmployee]
GO


