using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealesApi.DataResponse;
using RealesApi.DTA.Intefaces;
using RealesApi.DTO.PropertyTypeDTO;
using RealesApi.Helpers.HashService;
using RealesApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RealesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPropertyType _propType;
        private readonly ILogger<PropertyType> _logger;
        private readonly IHashService _hash;
        public PropertyTypeController(IUnitOfWork unitOfWork, IPropertyType propType, ILogger<PropertyType> logger, IHashService hash)
        {
            _unitOfWork = unitOfWork;
            _propType = propType;
            _logger = logger;
            _hash = hash;
        }

        [HttpGet]
        [Route("GetAllPropTypes")]
        public async Task<ActionResult<PropertyTypeDTO>> GetAllPropertyTypes(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _propType.GetAllProperties(cancellationToken);
                await _unitOfWork.Save(cancellationToken);
                return Ok(response);
            }
            catch (Exception)
            {
                //_logger.Error(e, "Register POST request");
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = "Couldn't find any property types."

                };
                return BadRequest(errRet);
            }
        }
        [HttpGet]
        [Route("GetPropertyTypeById")]
        public async Task<ActionResult<string>> GetPropertyTypeById(Guid propTypeId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _propType.GetPropertyTypeById(propTypeId, cancellationToken);
                await _unitOfWork.Save(cancellationToken);
                return Ok(response);
            }
            catch (Exception)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = "Couldn't find property type name with the given property type id."

                };
                return BadRequest(errRet);
            }
        }
    }
}

