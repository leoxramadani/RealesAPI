﻿using System;
using System.Collections.Generic;
using RealesApi.Models.Shared;

#nullable disable

namespace RealesApi.Models
{
    public partial class User : BaseEntity
    {
        public User()
        {
            Properties = new HashSet<Property>();
            SaveProperty = new HashSet<SaveProperty>();
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<SaveProperty> SaveProperty { get; set; }
    }
}
