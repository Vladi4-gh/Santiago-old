using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Santiago.Infrastructure.Repositories.DataProviding.DbEntities
{
    [Table("dbo.PageTemplate")]
    public class DbPageTemplate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }
    }
}