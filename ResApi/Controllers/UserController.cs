using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealesApi.DataResponse;
using RealesApi.DTA.Intefaces;
using RealesApi.DTA.Services;
using RealesApi.DTO.UserDTO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RealesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUser userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var user = _userService.GetCurrentUser(User);
            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }
        [HttpGet("GetUserById")]
        public async Task<ActionResult<UserDTO>> GetUserById(Guid Id,CancellationToken cancellationToken)
        {
            try
            {
                var response = await _userService.GetUserById(Id, cancellationToken);
                await _unitOfWork.Save(cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message,
                };
                return BadRequest(errorResponse);
            }
        }
    }
}

