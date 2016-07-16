using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Santiago.Infrastructure.Repositories.DataProviding.DbEntities
{
    [Table("dbo.MainMenuItem")]
    public class DbMainMenuItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public int Order { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}