using System;

namespace Santiago.Web.Areas.Admin.ViewModels.User
{
    public class UserDataTableItemViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}