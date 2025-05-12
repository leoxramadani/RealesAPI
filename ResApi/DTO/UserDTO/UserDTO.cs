using System;
namespace RealesApi.DTO.UserDTO
{
	public class UserDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime DateRegistered { get; set; }
	}
}

