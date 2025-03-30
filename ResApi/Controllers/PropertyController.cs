using System;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealesApi.DataResponse;
using RealesApi.DTA.Intefaces;
using RealesApi.DTO.Property;
using RealesApi.Helpers.HashService;
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
        private readonly IHashService _hash;
        public PropertyController(IUnitOfWork unitOfWork, IProperty prop, ILogger<Property> logger, IHashService hash)
        {
            _unitOfWork = unitOfWork;
            _prop = prop;
            _logger = logger;
            _hash = hash;
        }
        [HttpGet]
        [Route("GetAllProperties")]
        public async Task<ActionResult<PropertyDTO>> GetAllProperties(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _prop.GetAllProperties(cancellationToken);
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

        [HttpGet]
        [Route("GetPropertyById")]
        public async Task<ActionResult<PropertyDTO>> GetPropertyById(Guid propId, CancellationToken cancellationToken)
        {
            try
            {
                var property = await _prop.GetPropertyById(propId, cancellationToken);
                await _unitOfWork.Save(cancellationToken);
                return Ok(property);
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = "Couldn't find your property."

                };
                return BadRequest(errRet);
            }

        }

        [HttpPost]
        [Route("CreateNewProperty")]
        public async Task<ActionResult<PropertyDTO>> CreateNewProperty(Property entity,CancellationToken cancellationToken)
        {
            try
            {
                var property = await _prop.CreateNewProperty(entity);
                await _unitOfWork.Save(cancellationToken);
                return Ok(property);
            }
            catch (Exception)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = "Couldn't create new property."

                };
                return BadRequest(errRet);
            }
        }
        [HttpPut]
        [Route("DeleteProperty")]
        public async Task<ActionResult<PropertyDTO>> SoftDeleteProperty(Guid propId, CancellationToken cancellationToken)
        {
            try
            {
                var property = await _prop.SoftDeleteProperty(propId, cancellationToken);
                await _unitOfWork.Save(cancellationToken);
                return Ok(property);
            }
            catch (Exception)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = "Couldn't delete your property."

                };
                return BadRequest(errRet);
            }
        }

    }
}