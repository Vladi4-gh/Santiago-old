CREATE TABLE [dbo].[Testimonial]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[AuthorName] NVARCHAR(256) NOT NULL,
	[AuthorImageFileId] INT NOT NULL,
	[Text] NVARCHAR(MAX) NOT NULL,
	[CreationDate] DATETIME2(7) NOT NULL,
	[LastModifiedDate] DATETIME2(7) NOT NULL,
	CONSTRAINT [PK_dbo.Testimonial_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Testimonial_AuthorImageFileId__dbo.ImageFile_Id] FOREIGN KEY ([AuthorImageFileId]) REFERENCES [dbo].[ImageFile] ([Id])
);