using System.Collections.Generic;

namespace Santiago.Web.Areas.Admin.ViewModels.Common
{
    public class DataTableViewModel<TItemsTotalCount, TItem>
    {
        public TItemsTotalCount ItemsTotalCount { get; set; }

        public List<TItem> Items { get; set; }
    }
}