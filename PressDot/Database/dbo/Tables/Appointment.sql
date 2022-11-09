CREATE TABLE [dbo].[Appointment](
	[AppointmentId]		[int] IDENTITY(1,1) NOT NULL,
	[CustomerId]		[int] NOT NULL,
	[DoctorId]			[int] NOT NULL,
	[SaloonId]			[int] NOT NULL,
	[StateId]			[int] NOT NULL,
	[Symptoms]			[nvarchar](max) NULL,
	[Date]				DATETIME NOT NULL,
	[StartTime]			[time](7) NOT NULL,
	[EndTime]			[time](7) NOT NULL,
	[CreatedDate]		[datetime] NOT NULL,
	[CreatedBy]			INT NULL,
	[UpdatedDate]		[datetime] NULL,
	[UpdatedBy]			INT NULL,
	[DeletedDate]		[datetime] NULL,
	[DeletedBy]			INT NULL,
	[IsDeleted]			bit NOT NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[AppointmentId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Appointment] ADD  CONSTRAINT [DF_Appointment_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[Appointment] ADD  CONSTRAINT [DF_Appointment_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Saloon] FOREIGN KEY([SaloonId])
REFERENCES [dbo].[Saloon] ([SaloonId])
GO

ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Saloon]
GO

ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_States] FOREIGN KEY([StateId])
REFERENCES [dbo].[States] ([StateId])
GO

ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_States]
GO

ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Users] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Users]
GO

ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Users1] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Users1]
GO



