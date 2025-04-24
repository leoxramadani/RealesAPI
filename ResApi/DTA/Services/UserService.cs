using System;
using System.Linq;
using System.Security.Claims;
using RealesApi.DTA.Intefaces;
using RealesApi.DTO.Kinde;
using RealesApi.Models;

namespace RealesApi.DTA.Services
{
	public class UserService : IUser 
	{
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }
        public KindeUser GetCurrentUser(ClaimsPrincipal user)
        {
            if (user?.Identity?.IsAuthenticated != true)
            {
                return null;
            }
            return new KindeUser
            {
                Id = user.FindFirstValue("sub"),
                Email = user.FindFirstValue("email"),
                OrgCode = user.FindFirstValue("org_code"),
                OrgName = user.FindFirstValue("org_name"),
                Scopes = user.Claims
                    //.Where(c => c.Type == "scp")
                    .Select(c => c.Value)
                    .ToList()
            };
        }
    }
}

