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
using RealesApi.DTO.WhatsSpecialDTO;
using RealesApi.Models;

namespace RealesApi.DTA.Services
{
    public class WhatsSpecialPropertyLinkService : BaseService<PropertyWhatsSpecialLink>, IWhatsSpecialPropertyLink
    {
        private readonly ILogger<Property> _logger;

        private readonly IMapper _mapper;
        private readonly DataContext _context;


        //GetAllWhatsSpecial
        public WhatsSpecialPropertyLinkService(DataContext context, ILogger<Property> logger, IMapper mapper)
            : base(context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<WhatsSpecialLinkDTO>> GetAllSpecials(CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.PropertyWhatsSpecialLinks
                                           .Include(x=>x.Property)
                                           .Include(x=>x.WhatsSpecial)
                                           .Where(x=>x.Deleted != true || x.Property.Deleted != true)
                                           .Select(x => _mapper.Map<WhatsSpecialLinkDTO>(x))
                                           .ToListAsync(cancellationToken);


                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
        //GetWhatsSpecialById
        public async Task<List<WhatsSpecialLinkDTO>> GetSpecialsById(Guid propId, CancellationToken cancellationToken)
        {
            try
            {
                if (propId == Guid.Empty || string.IsNullOrEmpty(propId.ToString()))
                {
                    throw new Exception("PropertyId is null");
                }


                var property = await _context.PropertyWhatsSpecialLinks
                                           .Include(x => x.Property)
                                           .Include(x=>x.WhatsSpecial)
                                           .Where(x => x.Deleted != true && x.PropertyId == propId)
                                           .Select(x => _mapper.Map<WhatsSpecialLinkDTO>(x))
                                           .ToListAsync(cancellationToken);

                if (property == null)
                    return null;

                return property;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ex=", ex.Message);
                throw;
            }
        }
    }
}

