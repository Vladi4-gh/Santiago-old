using System;

namespace Santiago.Core.Entities
{
    public class PhotographCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public int Order { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}