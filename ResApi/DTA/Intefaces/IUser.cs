using RealesApi.DTO.Kinde;
using RealesApi.DTO.UserDTO;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RealesApi.DTA.Intefaces
{
	public interface IUser
	{
        KindeUser GetCurrentUser(ClaimsPrincipal user);
        Task<UserDTO> GetUserById(Guid Id, CancellationToken cancellationToken);
    }
}

