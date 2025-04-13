using System;
using System.Collections.Generic;
using RealesApi.Models.Shared;

#nullable disable

namespace RealesApi.Models
{
    public partial class Property : BaseEntity
    {
        public Property()
        {
            PropertyOtherImages = new HashSet<PropertyOtherImage>();
            PropertyWhatsSpecialLinks = new HashSet<PropertyWhatsSpecialLink>();
            SaveProperty = new HashSet<SaveProperty>();
        }

        public string Name { get; set; }
        public Guid? ReviewsRates { get; set; }
        public int? Views { get; set; }
        public DateTime? DatePosted { get; set; }
        public int? Saves { get; set; }
        public string MainImage { get; set; }
        public decimal Price { get; set; }
        public int? Rooms { get; set; }
        public int? Baths { get; set; }
        public int? Size { get; set; }
        public string BuiltIn { get; set; }
        public bool? AllDocuments { get; set; }
        public bool? MonthlyPayment { get; set; }
        public Guid ConditionId { get; set; }
        public Guid SellerId { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public Guid PropertyTypeId { get; set; }
        public Guid PurposeId { get; set; }
        public string PriceRange { get; set; }

        public virtual Condition Condition { get; set; }
        public virtual PropertyType PropertyType { get; set; }
        public virtual Purpose Purpose { get; set; }
        public virtual User Users { get; set; }
        public virtual ICollection<PropertyOtherImage> PropertyOtherImages { get; set; }
        public virtual ICollection<PropertyWhatsSpecialLink> PropertyWhatsSpecialLinks { get; set; }
        public virtual ICollection<SaveProperty> SaveProperty { get; set; }

    }
}
