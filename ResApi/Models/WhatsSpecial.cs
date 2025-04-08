using System;
using System.Collections.Generic;
using RealesApi.Models.Shared;
#nullable disable

namespace RealesApi.Models
{
    public partial class WhatsSpecial : BaseEntity
    {
        public WhatsSpecial()
        {
            PropertyWhatsSpecialLinks = new HashSet<PropertyWhatsSpecialLink>();
        }

        public string Name { get; set; }

        public virtual ICollection<PropertyWhatsSpecialLink> PropertyWhatsSpecialLinks { get; set; }
    }
}
