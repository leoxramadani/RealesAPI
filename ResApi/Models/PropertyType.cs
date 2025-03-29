using System;
using System.Collections.Generic;
using RealesApi.Models.Shared;
#nullable disable

namespace RealesApi.Models
{
    public partial class PropertyType : BaseEntity
    {
        public PropertyType()
        {
            Properties = new HashSet<Property>();
        }

        public string Name { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
