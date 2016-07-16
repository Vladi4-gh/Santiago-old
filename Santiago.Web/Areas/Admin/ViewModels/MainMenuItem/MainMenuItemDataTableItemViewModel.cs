using System;

namespace Santiago.Web.Areas.Admin.ViewModels.MainMenuItem
{
    public class MainMenuItemDataTableItemViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public int Order { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}