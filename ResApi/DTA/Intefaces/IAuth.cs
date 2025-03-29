using RealesApi.DTO.LoginDTO;

namespace RealesApi.DTA.Intefaces
{
    public interface IAuth
	{
        UserDTO AuthenticateUser(UserLoginDTO userLogin);

    }
}

