using System;
using System.Collections.Generic;
using ResApi.Models.Shared;

#nullable disable

namespace ResApi.Models
{
    public partial class User : BaseEntity
    {
        public User()
        {
            Properties = new HashSet<Property>();
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }


        public virtual ICollection<Property> Properties { get; set; }
    }
}
