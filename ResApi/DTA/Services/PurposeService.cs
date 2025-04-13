using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealesApi.DTA.Intefaces;
using RealesApi.DTA.Services.Shared;
using RealesApi.DTO.PurposeDTO;
using RealesApi.Models;

namespace RealesApi.DTA.Services
{
    public class PurposeService : BaseService<Property>, IPurpose
    {
        private readonly ILogger<Purpose> _logger;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public PurposeService(DataContext context, ILogger<Purpose> logger, IMapper mapper)
            : base(context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<PurposeDTO>> GetAllPurposes(CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.Purposes
                                           .Select(x => _mapper.Map<PurposeDTO>(x))
                                           .ToListAsync(cancellationToken);


                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
    }
}

