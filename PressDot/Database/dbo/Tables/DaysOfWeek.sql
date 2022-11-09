CREATE TABLE [dbo].[DaysOfWeek](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Day] [nvarchar](50) NOT NULL,
	[CreatedDate]			DATETIME			CONSTRAINT [DF_DaysOfWeek_CreatedDate]  DEFAULT (GETDATE()) NOT NULL,
	[CreatedBy]				INT					NULL,
	[UpdatedDate]			DATETIME			NULL,
	[UpdatedBy]				INT					NULL,
	[DeletedDate]			DATETIME			NULL,
	[DeletedBy]				INT					NULL,
	[IsDeleted]				bit					CONSTRAINT [DF_DaysOfWeek_IsDeleted] DEFAULT ((0)) NOT NULL,
 CONSTRAINT [PK_DaysOfWeek] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]