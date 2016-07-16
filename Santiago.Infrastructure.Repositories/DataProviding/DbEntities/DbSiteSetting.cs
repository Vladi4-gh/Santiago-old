using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Santiago.Infrastructure.Repositories.DataProviding.DbEntities
{
    [Table("dbo.SiteSetting")]
    public class DbSiteSetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}