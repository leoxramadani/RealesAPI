using System;
using System.ComponentModel.DataAnnotations;

namespace ResApi.Models.Shared
{
	public class BaseEntity
	{
        [Key]
        public Guid Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
    }
}

