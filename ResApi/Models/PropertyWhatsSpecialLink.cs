using System;
using System.Collections.Generic;
using ResApi.Models.Shared;
#nullable disable

namespace ResApi.Models
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
