using System;
using System.Collections.Generic;
using ResApi.Models.Shared;
#nullable disable

namespace ResApi.Models
{
    public partial class WhatsSpecial : BaseEntity
    {
        public WhatsSpecial()
        {
            Properties = new HashSet<Property>();
            PropertyWhatsSpecialLinks = new HashSet<PropertyWhatsSpecialLink>();
        }

        public string Name { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<PropertyWhatsSpecialLink> PropertyWhatsSpecialLinks { get; set; }
    }
}
