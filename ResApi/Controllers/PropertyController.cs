using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealesApi.DataResponse;
using RealesApi.DTA.Intefaces;
using RealesApi.DTO.Property;
using RealesApi.DTO.SavePropertyDTO;
using RealesApi.Helpers.HashService;
using RealesApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RealesApi.Controllers
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
        [Authorize]
        public async Task<ActionResult<PropertyDTO>> GetAllProperties(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _prop.GetAllProperties(cancellationToken);
                await _unitOfWork.Save(cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                //_logger.Error(e, "Register POST request");
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message

                };
                return BadRequest(errRet);
            }
        }
        [HttpGet]
        [Route("GetLatestTwoProperties")]
        public async Task<ActionResult<PropertyDTO>> GetLatestTwoProperties(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _prop.GetLatestTwoProperties(cancellationToken);
                await _unitOfWork.Save(cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message

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
                    ErrorMessage = ex.Message

                };
                return BadRequest(errRet);
            }
        }
        [HttpGet]
        [Route("GetPropertyBySellerId")]
        public async Task<ActionResult<PropertyDTO>> GetPropertyBySellerId(Guid sellerId, CancellationToken cancellationToken)
        {
            try
            {
                var property = await _prop.GetPropertyBySellerId(sellerId, cancellationToken);
                await _unitOfWork.Save(cancellationToken);
                return Ok(property);
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message

                };
                return BadRequest(errRet);
            }
        }

        [HttpGet]
        [Route("GetPropCountForSeller")]
        public async Task<ActionResult<PropertyDTO>> GetPropCountForSeller(Guid sellerId, CancellationToken cancellationToken)
        {
            try
            {
                var property = _prop.GetPropCountForSeller(sellerId);
                await _unitOfWork.Save(cancellationToken);
                return Ok(property);
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message

                };
                return BadRequest(errRet);
            }
        }
        [HttpGet]
        [Route("GetSavedPropertiesBySellerCount")]
        public async Task<ActionResult<PropertyDTO>> GetPropertySavedBySeller(Guid sellerId, CancellationToken cancellationToken)
        {
            try
            {
                var count = _prop.SavedPropertiesBySellerCount(sellerId);
                await _unitOfWork.Save(cancellationToken);
                return Ok(count);
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message,

                };
                return BadRequest(errRet);
            }
        }
        [HttpGet]
        [Route("GetPropertyForRentByUserId")]
        public async Task<ActionResult<List<PropertyDTO>>> GetPropertyForRentByUserId(Guid sellerId, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _prop.GetPropertyForRentByUserId(sellerId);
                await _unitOfWork.Save(cancellationToken);
                return entity;
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message

                };
                return BadRequest(errRet);
            }
        }
        [HttpGet]
        [Route("GetPropertyForSaleByUserId")]
        public async Task<ActionResult<List<PropertyDTO>>> GetPropertyForSaleByUserId(Guid sellerId,CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _prop.GetPropertyForSaleByUserId(sellerId);
                await _unitOfWork.Save(cancellationToken);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message

                };
                return BadRequest(errRet);
            }
        }

        [HttpPost]
        [Route("CreateNewProperty")]
        public async Task<ActionResult<PropertyDTO>> CreateNewProperty(PropertyDTO entity,CancellationToken cancellationToken)
        {
            try
            {
                var property = await _prop.CreateNewProperty(entity);
                await _unitOfWork.Save(cancellationToken);
                return Ok(property);
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message,

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
            catch (Exception ex)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message

                };
                return BadRequest(errRet);
            }
        }
        [HttpPost]
        [Route("SaveProperty")]
        public async Task<ActionResult<bool>> SaveProperty(SavePropertyDTO entity,CancellationToken cancellationToken)
        {
            try
            {
                var saveProperty = await _prop.SaveProperty(entity);
                await _unitOfWork.Save(cancellationToken);
                return Ok(saveProperty);
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message

                };
                return BadRequest(errRet);
            }
        }
        [HttpGet]
        [Route("GetPropertiesSavedBySellerId")]
        public async Task<ActionResult<List<PropertyDTO>>> GetPropertiesSavedBySellerId(Guid sellerId, CancellationToken cancellationToken)
        {
            try
            {
                var savedProps = await _prop.GetPropertiesSavedBySellerId(sellerId, cancellationToken);
                await _unitOfWork.Save(cancellationToken);
                return Ok(savedProps);
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message

                };
                return BadRequest(errRet);
            }
        }
        [HttpGet]
        [Route("GetPropertyByUserIdPending")]
        public async Task<ActionResult<List<PropertyDTO>>> GetPropertyByUserIdPending(Guid sellerId,CancellationToken cancellationToken)
        {
            try
            {
                var savedProps = await _prop.GetPropertyByUserIdPending(sellerId);
                await _unitOfWork.Save(cancellationToken);
                return Ok(savedProps);
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message

                };
                return BadRequest(errRet);
            }
        }

        [HttpGet]
        [Route("GetPropertyByUserIdPublished")]
        public async Task<ActionResult<List<PropertyDTO>>> GetPropertyByUserIdPublished(Guid sellerId, CancellationToken cancellationToken)
        {
            try
            {
                var savedProps = await _prop.GetPropertyByUserIdPublished(sellerId);
                await _unitOfWork.Save(cancellationToken);
                return Ok(savedProps);
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message

                };
                return BadRequest(errRet);
            }
        }

        [HttpGet]
        [Route("GetPropertyByUserIdRejected")]
        public async Task<ActionResult<List<PropertyDTO>>> GetPropertyByUserIdRejected(Guid sellerId, CancellationToken cancellationToken)
        {
            try
            {
                var savedProps = await _prop.GetPropertyByUserIdRejected(sellerId);
                await _unitOfWork.Save(cancellationToken);
                return Ok(savedProps);
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message

                };
                return BadRequest(errRet);
            }
        }
        [HttpPost]
        [Route("SearchProperties")]
        public async Task<ActionResult<List<PropertyDTO>>> SearchProperties([FromBody] PropertySearchDTO searchDto, CancellationToken cancellationToken)
        {
            try
            {
                var results = await _prop.SearchProperties(searchDto, cancellationToken);
                return Ok(results);
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
                return BadRequest(errRet);
            }
        }
        [HttpPost]
        [Route("SearchPropertiesGetFiveRows")]
        public async Task<ActionResult<List<PropertyDTO>>> SearchPropertiesGetFiveRows([FromBody] PropertySearchDTO searchDto, CancellationToken cancellationToken)
        {
            try
            {
                var results = await _prop.SearchPropertiesGetFiveRows(searchDto, cancellationToken);
                return Ok(results);
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<bool>
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
                return BadRequest(errRet);
            }
        }


    }
}