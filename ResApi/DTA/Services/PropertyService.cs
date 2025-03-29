using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResApi.DTA.Intefaces;
using ResApi.DTA.Services.Shared;
using ResApi.DTO.Property;
using ResApi.DTO.Tables;
using ResApi.Models;

namespace ResApi.DTA.Services
{
    public class PropertyService : BaseService<Property>, IProperty
    {
        private readonly ILogger<Property> _logger;
         
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public PropertyService(DataContext context, ILogger<Property> logger, IMapper mapper) 
            : base(context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<PropertyDTO>> GetAllProperties(CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.Properties
                                           .Include(x => x.PropertyOtherImages)
                                           .Include(x => x.Condition)
                                           .Include(x => x.Users)
                                           .Include(x => x.WhatsSpecial)
                                           .Include(x => x.PropertyType)
                                           .Select(x => _mapper.Map<PropertyDTO>(x))
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


