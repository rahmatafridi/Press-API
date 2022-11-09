CREATE TABLE [dbo].[Location](
	[LocationId]		INT					IDENTITY(1,1)	NOT NULL,
	[LocationName]		NVARCHAR(200)		NOT NULL,
	[ParentLocationId]	INT					NULL,
	[CreatedDate]		DATETIME			CONSTRAINT [DF_Location_CreatedDate]  DEFAULT (GETDATE()) NOT NULL,
	[CreatedBy]			INT					NULL,
	[UpdatedDate]		DATETIME			NULL,
	[UpdatedBy]			INT					NULL,
	[DeletedDate]		DATETIME			NULL,
	[DeletedBy]			INT					NULL,
	[IsDeleted]			BIT					CONSTRAINT [DF_Location_IsDeleted] DEFAULT ((0)) NOT NULL,
	CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([LocationId] ASC),
    CONSTRAINT [FK_Location_Location] FOREIGN KEY ([ParentLocationId]) REFERENCES [dbo].[Location] ([LocationId])
);
