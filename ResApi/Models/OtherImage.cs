using System;
using System.Collections.Generic;
using RealesApi.Models.Shared;

#nullable disable

namespace RealesApi.Models
{
    public partial class OtherImage : BaseEntity
    {
        public OtherImage()
        {
            PropertyOtherImages = new HashSet<PropertyOtherImage>();
        }

        public string Base64stringImage { get; set; }

        public virtual ICollection<PropertyOtherImage> PropertyOtherImages { get; set; }
    }
}
