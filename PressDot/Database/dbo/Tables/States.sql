CREATE TABLE [dbo].[States](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](250) NOT NULL,
	[StateFor] [nvarchar](250) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] NVARCHAR(250) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] NVARCHAR(250) NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] NVARCHAR(250) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[States] ADD  CONSTRAINT [DF_States_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[States] ADD  CONSTRAINT [DF_States_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
