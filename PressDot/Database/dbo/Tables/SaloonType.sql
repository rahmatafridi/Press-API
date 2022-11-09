CREATE TABLE [dbo].[SaloonType]
(
	[SaloonTypeId]				INT				NOT NULL PRIMARY KEY IDENTITY, 
    [SaloonTypeName]			NVARCHAR(50)	NOT NULL,
	[CreatedDate]				DATETIME		CONSTRAINT [DF_SaloonTypeName_CreatedDate]  DEFAULT (GETDATE()) NOT NULL,
	[CreatedBy]					INT				NULL,
	[UpdatedDate]				DATETIME		NULL,
	[UpdatedBy]					INT				NULL,
	[DeletedDate]				DATETIME		NULL,
	[DeletedBy]					INT				NULL,
	[IsDeleted]					BIT				CONSTRAINT [DF_SaloonTypeName_IsDeleted] DEFAULT ((0)) NOT NULL
)
