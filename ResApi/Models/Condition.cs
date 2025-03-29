using System;
using System.Collections.Generic;
using RealesApi.Models.Shared;
#nullable disable

namespace RealesApi.Models
{
    public partial class Condition : BaseEntity
    {
        public Condition()
        {
            Properties = new HashSet<Property>();
        }

        public string Name { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
