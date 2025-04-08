using System;
using RealesApi.Models.Shared;

namespace RealesApi.Models
{
	public class SaveProperty : BaseEntity
	{
		public SaveProperty()
		{
		}

		public Guid SellerId { get; set; }
		public Guid PropertyId { get; set; }

		public virtual User Users { get; set; }
        public virtual Property Property { get; set; }
    }
}

