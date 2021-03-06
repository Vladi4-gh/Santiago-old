﻿using System;

namespace Santiago.Web.ViewModels.FileUpload
{
    public class UploadedImageFileViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public long Length { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public DateTime UploadDate { get; set; }
    }
}