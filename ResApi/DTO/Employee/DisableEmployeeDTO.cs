﻿using System;
namespace ResApi.DTO
{
	public class DisableEmployeeDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public bool? Status { get; set; }
    }
}

