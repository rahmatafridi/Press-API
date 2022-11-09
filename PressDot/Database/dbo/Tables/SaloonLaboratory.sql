CREATE TABLE [dbo].[SaloonLaboratory]
(
	[SaloonLaboratoryId]			INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[SaloonId]						INT NOT NULL,
	[LaboratoryId]					INT NOT NULL,
	[CreatedDate]					DATETIME CONSTRAINT [DF_SaloonLaboratory_CreatedDate]  DEFAULT (GETDATE()) NOT NULL,
	[CreatedBy]						INT NULL,
	[UpdatedDate]					DATETIME NULL,
	[UpdatedBy]						INT NULL,
	[DeletedDate]					DATETIME NULL,
	[DeletedBy]						INT NULL,
	[IsDeleted]						BIT CONSTRAINT [DF_SaloonLaboratory_IsDeleted] DEFAULT ((0)) NOT NULL,
	[IsDefault] BIT NOT NULL, 
    CONSTRAINT [FK_SaloonLaboratory_Saloon] FOREIGN KEY ([SaloonId]) REFERENCES [dbo].[Saloon] ([SaloonId]),
	CONSTRAINT [FK_SaloonLaboratory_Laboratory] FOREIGN KEY ([LaboratoryId]) REFERENCES [dbo].[Laboratory] ([LaboratoryId])
)
