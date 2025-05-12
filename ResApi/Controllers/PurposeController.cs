using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealesApi.DataResponse;
using RealesApi.DTA.Intefaces;
using RealesApi.DTO.PurposeDTO;
using RealesApi.Helpers.HashService;
using RealesApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RealesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PurposeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPurpose _purpose;
        private readonly ILogger<Purpose> _logger;
        private readonly IHashService _hash;
        public PurposeController(IUnitOfWork unitOfWork, IPurpose purpose, ILogger<Purpose> logger, IHashService hash)
        {
            _unitOfWork = unitOfWork;
            _purpose = purpose;
            _logger = logger;
            _hash = hash;
        }

        [HttpGet]
        [Route("GetAllPurposes")]
        public async Task<ActionResult<PurposeDTO>> GetAllPurposes(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _purpose.GetAllPurposes(cancellationToken);
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

