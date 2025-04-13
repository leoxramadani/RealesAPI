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
using RealesApi.DTO.ConditionsDTO;
using RealesApi.Models;

namespace RealesApi.DTA.Services
{
    public class ConditionsServices : BaseService<Condition>, IConditions
    {
        private readonly ILogger<Condition> _logger;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ConditionsServices(DataContext context, ILogger<Condition> logger, IMapper mapper)
            : base(context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }
        //Get all conditions
        public async Task<List<ConditionsDTO>> GetAllConditions(CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.Conditions
                                           .Select(x => _mapper.Map<ConditionsDTO>(x))
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

