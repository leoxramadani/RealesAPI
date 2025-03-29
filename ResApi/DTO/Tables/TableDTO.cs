using System;
namespace ResApi.DTO.Tables
{
	public class TableDTO
	{
        public Guid Id { get; set; }
        public int? TableNumber { get; set; }
        public int? Seats { get; set; }
    }
}