using System;
using System.Collections.Generic;
using ResApi.Models.Shared;

#nullable disable

namespace ResApi.Models
{
    public partial class PropertyOtherImage : BaseEntity
    {
        public Guid OtherImagesId { get; set; }
        public Guid PropertyId { get; set; }

        public virtual OtherImage OtherImages { get; set; }
        public virtual Property Property { get; set; }
    }
}
