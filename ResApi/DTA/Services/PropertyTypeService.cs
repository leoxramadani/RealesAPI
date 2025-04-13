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
using RealesApi.DTO.PropertyTypeDTO;
using RealesApi.Models;

namespace RealesApi.DTA.Services
{
    public class PropertyTypeService : BaseService<PropertyType> , IPropertyType
    {
        private readonly ILogger<PropertyType> _logger;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public PropertyTypeService(DataContext context, ILogger<PropertyType> logger, IMapper mapper)
            : base(context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }
        //Get all properties
        public async Task<List<PropertyTypeDTO>> GetAllProperties(CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.PropertyTypes
                                           .Select(x => _mapper.Map<PropertyTypeDTO>(x))
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

