using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealesApi.DTA.Intefaces;
using RealesApi.DTO.Kinde;
using RealesApi.DTO.UserDTO;
using RealesApi.Models;

namespace RealesApi.DTA.Services
{
	public class UserService : IUser 
	{
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        public async Task<UserDTO> GetUserById(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                if (Id == Guid.Empty || string.IsNullOrEmpty(Id.ToString()))
                {
                    throw new Exception("Id is null");
                }

                var userinfo = await _context.Users
                                     .Where(x => x.Id == Id && x.Deleted != true)
                                     .Select(x => _mapper.Map<UserDTO>(x))
                                     .FirstOrDefaultAsync(cancellationToken);

                return userinfo; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null; 
            }
        }

    }
}

