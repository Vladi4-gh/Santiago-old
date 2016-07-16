using System;

namespace Santiago.Core.Entities
{
    public class MainMenuItem
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public int Order { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}