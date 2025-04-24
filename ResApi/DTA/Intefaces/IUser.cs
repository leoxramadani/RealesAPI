using RealesApi.DTO.Kinde;
using System.Security.Claims;

namespace RealesApi.DTA.Intefaces
{
	public interface IUser
	{
        KindeUser GetCurrentUser(ClaimsPrincipal user);
    }
}

