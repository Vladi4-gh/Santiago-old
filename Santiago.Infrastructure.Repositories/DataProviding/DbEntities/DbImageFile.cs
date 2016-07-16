using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Santiago.Infrastructure.Repositories.DataProviding.DbEntities
{
    [Table("dbo.ImageFile")]
    public class DbImageFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public long Length { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public DateTime UploadDate { get; set; }
    }
}