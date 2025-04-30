using System;
namespace RealesApi.DTO.Property
{
        public class PropertySearchDTO
        {
            public string? Name { get; set; }
            public string? Location { get; set; }
            public int? Floors { get; set; }
            public int? MinSize { get; set; }
            public int? MaxSize { get; set; }
            public string? PropertyTypeName { get; set; }
            public decimal? MinPrice { get; set; }
            public decimal? MaxPrice { get; set; }
            public int? RentFrame { get; set; }
    }
}

