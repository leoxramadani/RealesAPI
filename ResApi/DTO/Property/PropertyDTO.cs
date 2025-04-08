using System;
using System.Collections.Generic;
using RealesApi.DTO.WhatsSpecialDTO;

namespace RealesApi.DTO.Property
{
	public class PropertyDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
        public Guid ReviewsRates { get; set; }
        public int Views { get; set; }
        public DateTime DatePosted { get; set; }
        public int Saves { get; set; }
        public string MainImage { get; set; }
        public Guid OtherImagesId{ get; set; }
        public decimal Price { get; set; }
        public int Rooms{ get; set; }
        public int Baths{ get; set; }
        public int Size{ get; set; }
        public string BuiltIn{ get; set; }
        public bool AllDocuments{ get; set; }
        public bool MonthlyPayment{ get; set; }

        public string ConditionName { get; set; }
        public Guid ConditionId { get; set; }
        public Guid PropertyTypeId { get; set; }

        public Guid UserId { get; set; }
        public Guid SellerId { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string  UserEmail { get; set; }
        public string UserPhone { get; set; }

        public string Description { get; set; }
        public string Location { get; set; }
        public string PropertyTypeName { get; set; }

        public string PurposeName{ get; set; }
        public Guid PurposeId { get; set; }
        public string PriceRange { get; set; }
        public List<WhatsSpecialLinkDTO> WhatsSpecialNames { get; set; }


        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool Deleted { get; set; } = false;
    }
}

