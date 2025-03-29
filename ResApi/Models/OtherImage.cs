using System;
using System.Collections.Generic;
using ResApi.Models.Shared;

#nullable disable

namespace ResApi.Models
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
