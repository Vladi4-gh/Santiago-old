using System;

namespace Santiago.Web.Areas.Admin.ViewModels.PhotographCategory
{
    public class PhotographCategoryDataTableItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public int Order { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}