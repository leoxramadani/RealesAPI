using System;
using System.Collections.Generic;
using RealesApi.Models.Shared;

#nullable disable

namespace RealesApi.Models
{
    public partial class PropertyOtherImage : BaseEntity
    {
        public Guid OtherImagesId { get; set; }
        public Guid PropertyId { get; set; }

        public virtual OtherImage OtherImages { get; set; }
        public virtual Property Property { get; set; }
    }
}
