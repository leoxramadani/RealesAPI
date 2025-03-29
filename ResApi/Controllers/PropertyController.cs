using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealesApi.DataResponse;
using RealesApi.DTA.Intefaces;
using RealesApi.DTO.Property;
using RealesApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProperty _prop;
        private readonly ILogger<Property> _logger;
        public PropertyController(IUnitOfWork unitOfWork, IProperty prop, ILogger<Property> logger)
        {
            _unitOfWork = unitOfWork;
            _prop = prop;
            _logger = logger;
        }
        [HttpGet]
        [Route("GetAllProperties")]
        public async Task<ActionResult<PropertyDTO>> GetAllProperties(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _prop.GetAllProperties(cancellationToken);
                return Ok(response);
            }
            catch (Exception)
            {
                //_logger.Error(e, "Register POST request");
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = "Couldnt find any employees"

                };
                return BadRequest(errRet);
            }
            //return Ok(await _emp.Get(empId, cancellationToken));
        }

    }
}