CREATE TABLE [dbo].[Laboratory]
(
	[LaboratoryId]			INT					PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[LaboratoryName]		nvarchar(200)		NOT NULL,
	[CreatedDate]			DATETIME			CONSTRAINT [DF_Laboratory_CreatedDate]  DEFAULT (GETDATE()) NOT NULL,
	[CreatedBy]				INT					NULL,
	[UpdatedDate]			DATETIME			NULL,
	[UpdatedBy]				INT					NULL,
	[DeletedDate]			DATETIME			NULL,
	[DeletedBy]				INT					NULL,
	[IsDeleted]				bit					CONSTRAINT [DF_Laboratory_IsDeleted] DEFAULT ((0)) NOT NULL,
);
