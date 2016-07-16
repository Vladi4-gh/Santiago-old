CREATE TABLE [dbo].[Photograph]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[GalleryItemImageFileId] INT NOT NULL,
	[GallerySliderImageFileId] INT NOT NULL,
	[CategoryId] INT NULL,
	[Title] NVARCHAR(256) NOT NULL,
	[Description] NVARCHAR(256) NULL,
	[CreationDate] DATETIME2(7) NOT NULL,
	[LastModifiedDate] DATETIME2(7) NOT NULL,
	CONSTRAINT [PK_dbo.Photograph_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Photograph_GalleryItemImageFileId__dbo.ImageFile_Id] FOREIGN KEY ([GalleryItemImageFileId]) REFERENCES [dbo].[ImageFile] ([Id]),
	CONSTRAINT [FK_dbo.Photograph_GallerySliderImageFileId__dbo.ImageFile_Id] FOREIGN KEY ([GallerySliderImageFileId]) REFERENCES [dbo].[ImageFile] ([Id]),
	CONSTRAINT [FK_dbo.Photograph_CategoryId__dbo.PhotographCategory_Id] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[PhotographCategory] ([Id]) ON DELETE SET NULL
);