﻿namespace ResApi.DTO
{
    public class MenuItemDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? CategoryName { get; set; }   
        public int? CategoryId { get; set; }
        public int? Quantity { get; set; }
        public decimal? OrderPrice { get; set; }
    }
}
