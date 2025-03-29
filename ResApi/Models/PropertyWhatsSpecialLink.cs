using System;
using System.Collections.Generic;
using RealesApi.Models.Shared;
#nullable disable

namespace RealesApi.Models
{
    public partial class PropertyWhatsSpecialLink : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid WhatsSpecialId { get; set; }
        public Guid PropertyId { get; set; }

        public virtual Property Property { get; set; }
        public virtual WhatsSpecial WhatsSpecial { get; set; }
    }
}
