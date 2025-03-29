using System;
using System.Linq;
using ResApi.DTA.Intefaces;
using ResApi.DTO.LoginDTO;
using ResApi.Models;

namespace ResApi.DTA.Services
{
    public class AuthService : IAuth
	{
        private readonly DataContext _context;
        public AuthService(DataContext context) 
        {
            _context = context;
        }
        public UserDTO AuthenticateUser(UserLoginDTO userLogin)
        {
            try
            {
                var user = _context.Users.Where(x => x.Name == userLogin.Username && x.Name == userLogin.Password).FirstOrDefault();
                if (user != null)
                {
                    //var roleName = _context.Roles.Select(x => x.RoleName).FirstOrDefault();
                    var userDto = new UserDTO()
                    {
                        //Id = user.Id,
                        Name = user.Name,
                        Password = user.Name,
                        Username = user.Name,
                      //  RoleName = roleName,
                        Surname = user.LastName,

                    };
                    return userDto;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ex=", ex.Message);
                throw;
            }
        }
    }
}

