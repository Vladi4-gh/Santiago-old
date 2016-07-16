CREATE TABLE [dbo].[Page]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[Title] NVARCHAR(256) NOT NULL,
	[Alias] NVARCHAR(256) NOT NULL,
	[Text] NVARCHAR(MAX) NULL,
	[MetaDescription] NVARCHAR(256) NULL,
	[MetaKeywords] NVARCHAR(256) NULL,
	[ParentId] INT NULL,
	[SitemapXmlChangeFrequency] NVARCHAR(16) NULL,
	[SitemapXmlPriority] FLOAT(24) NULL,
	[TemplateId] INT NOT NULL,
	[IsPublished] BIT NOT NULL,
	[CreationDate] DATETIME2(7) NOT NULL,
	[LastModifiedDate] DATETIME2(7) NOT NULL,
	[PublicationDate] DATETIME2(7) NULL,
	CONSTRAINT [PK_dbo.Page_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Page_ParentId__dbo.Page_Id] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Page] ([Id]),
	CONSTRAINT [FK_dbo.Page_TemplateId__dbo.PageTemplate_Id] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[PageTemplate] ([Id])
);