using System;
namespace RealesApi.DTO.WhatsSpecialDTO
{
	public class WhatsSpecialLinkDTO
    {
		public Guid Id { get; set; }
		public string SpecialName { get; set; }
		public string PropertyName { get; set; }
		public Guid PropertyId { get; set; }
		public Guid WhatsSpecialId { get; set; }
	}
}

