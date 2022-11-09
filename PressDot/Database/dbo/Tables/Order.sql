CREATE TABLE [dbo].[Order](
	[OrderId] [int] NOT NULL IDENTITY,
	[AppointmentId] [int] NOT NULL,
	[LaboratoryId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] INT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] INT NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] INT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Appointment] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointment] ([AppointmentId])
GO

ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Appointment]
GO

ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Laboratory] FOREIGN KEY([LaboratoryId])
REFERENCES [dbo].[Laboratory] ([LaboratoryId])
GO

ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Laboratory]
GO

ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_States] FOREIGN KEY([StateId])
REFERENCES [dbo].[States] ([StateId])
GO

ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_States]
GO
