using System;
using System.Collections.Generic;
using RealesApi.Models.Shared;

#nullable disable

namespace RealesApi.Models
{
    public partial class PropertyOtherImage : BaseEntity
    {
        public Guid PropertyId { get; set; }
        public string Base64stringImage { get; set; }
        public virtual Property Property { get; set; }
    }
}
