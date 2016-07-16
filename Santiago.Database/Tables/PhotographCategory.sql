CREATE TABLE [dbo].[PhotographCategory]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[Name] NVARCHAR(256) NOT NULL,
	[Alias] NVARCHAR(256) NOT NULL,
	[Order] INT NOT NULL,
	[CreationDate] DATETIME2(7) NOT NULL,
	[LastModifiedDate] DATETIME2(7) NOT NULL,
	CONSTRAINT [PK_dbo.PhotographCategory_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);