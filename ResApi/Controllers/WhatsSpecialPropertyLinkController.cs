using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealesApi.DataResponse;
using RealesApi.DTA.Intefaces;
using RealesApi.DTO.Property;
using RealesApi.DTO.WhatsSpecialDTO;
using RealesApi.Helpers.HashService;
using RealesApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RealesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhatsSpecialPropertyLinkController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWhatsSpecialPropertyLink _whatsSpecial;
        private readonly ILogger<PropertyWhatsSpecialLink> _logger;
        private readonly IHashService _hash;
        public WhatsSpecialPropertyLinkController(IUnitOfWork unitOfWork, IWhatsSpecialPropertyLink whatsSpecial, ILogger<PropertyWhatsSpecialLink> logger, IHashService hash)
        {
            _unitOfWork = unitOfWork;
            _whatsSpecial = whatsSpecial;
            _logger = logger;
            _hash = hash;
        }

        [HttpGet]
        [Route("GetAllSpecialsLinks")]
        public async Task<ActionResult<WhatsSpecialLinkDTO>> GetAllSpecialsLinks(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _whatsSpecial.GetAllSpecialsLinks(cancellationToken);
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
        [Route("GetAllSpecials")]
        public async Task<ActionResult<WhatsSpecialLinkDTO>> GetAllSpecials(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _whatsSpecial.GetAllSpecials(cancellationToken);
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
        [Route("GetSpecialsById")]
        public async Task<ActionResult<PropertyDTO>> GetSpecialById(Guid propId, CancellationToken cancellationToken)
        {
            try
            {
                var property = await _whatsSpecial.GetSpecialsById(propId, cancellationToken);
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
        [Route("CreateWhatsSpecial")]
        public async Task<ActionResult<PropertyDTO>> CreateWhatsSpecial(WhatsSpecialDTO entity, CancellationToken cancellationToken)
        {
            try
            {
                var property = await _whatsSpecial.CreateWhatsSpecial(entity);
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
    }
}

