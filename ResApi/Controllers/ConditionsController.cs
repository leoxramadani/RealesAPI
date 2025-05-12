using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealesApi.DataResponse;
using RealesApi.DTA.Intefaces;
using RealesApi.DTO.ConditionsDTO;
using RealesApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RealesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConditionsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConditions _cond;
        private readonly ILogger<Condition> _logger;


        public ConditionsController(IUnitOfWork unitOfWork, IConditions cond, ILogger<Condition> logger)
        {
            _unitOfWork = unitOfWork;
            _cond = cond;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllConditions")]
        public async Task<ActionResult<ConditionsDTO>> GetAllPropertyTypes(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _cond.GetAllConditions(cancellationToken);
                await _unitOfWork.Save(cancellationToken);
                return Ok(response);
            }
            catch (Exception)
            {
                //_logger.Error(e, "Register POST request");
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = "Couldn't find any properties"

                };
                return BadRequest(errRet);
            }
        }
    }
}

