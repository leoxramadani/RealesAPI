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
using RealesApi.DTO.Property;
using RealesApi.Models;

namespace RealesApi.DTA.Services
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

        //Get all properties
        public async Task<List<PropertyDTO>> GetAllProperties(CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.Properties
                                           .Include(x => x.PropertyOtherImages)
                                           .Include(x => x.Condition)
                                           .Include(x => x.Users)
                                           .Include(x => x.PropertyWhatsSpecialLinks)
                                           .Include(x => x.PropertyType)
                                           .Include(x => x.Purpose)
                                           .Where(x => x.Deleted != true)
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

        //Get property by prop Id
        public async Task<PropertyDTO> GetPropertyById(Guid propId, CancellationToken cancellationToken)
        {
            try
            {
                if(propId == Guid.Empty || string.IsNullOrEmpty(propId.ToString()))
                {
                    throw new Exception("PropertyId is null");
                }


                var property = await _context.Properties
                                          .Include(x => x.PropertyOtherImages)
                                           .Include(x => x.Condition)
                                           .Include(x => x.Users)
                                           .Include(x => x.PropertyWhatsSpecialLinks)
                                           .Include(x => x.PropertyType)
                                           .Where(x => x.Id == propId && x.Deleted != true)
                                           .Select(x => _mapper.Map<PropertyDTO>(x))
                                           .FirstOrDefaultAsync(cancellationToken);

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

        public async Task<PropertyDTO> CreateNewProperty(PropertyDTO entity)
        {
            try
            {
                if(entity == null)
                {
                    return null;
                }
                Property prop = new()
                {
                    CreatedAt = DateTime.Now,
                    CreatedBy = "Admin",
                    Deleted = false,
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = "Admin",
                    
                    AllDocuments = entity.AllDocuments,
                    Baths = entity.Baths,
                    BuiltIn = entity.BuiltIn,
                    ConditionId = entity.ConditionId,
                    DatePosted = DateTime.Now,
                    Description = entity.Description,
                    Location = entity.Location,
                    MainImage = entity.MainImage,
                    MonthlyPayment = entity.MonthlyPayment,
                    Name = entity.Name,
                    Price = entity.Price,
                    PriceRange = entity.PriceRange,
                    PropertyTypeId = entity.PropertyTypeId,
                    PurposeId = entity.PurposeId,
                    ReviewsRates = entity.ReviewsRates,
                    Rooms = entity.Rooms,
                    Saves = entity.Saves,
                    Size = entity.Saves,
                    SellerId = entity.SellerId,
                    Views = entity.Views,
                };

                _context.Properties.Add(prop);
                await _context.SaveChangesAsync();

                var propertyId = _context.Properties.Where(x => x.Id == entity.SellerId).OrderByDescending(x => x.CreatedAt).Select(x=>x.Id).First();

                if(entity.WhatsSpecialNames.Count > 1)
                {
                    PropertyWhatsSpecialLink pwsl = new()
                    {
                        PropertyId = propertyId
                    };
                    foreach (var item in entity.WhatsSpecialNames)
                    {
                        pwsl.WhatsSpecialId = item.Id;
                        _context.PropertyWhatsSpecialLinks.Add(pwsl);
                        await _context.SaveChangesAsync();
                    }
                }

                var propMapped = _mapper.Map<PropertyDTO>(prop);

                return propMapped;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ex=", ex.Message);
                throw;
            }
        }

        public async Task<PropertyDTO> SoftDeleteProperty(Guid propId, CancellationToken cancellationToken)
        {
            try
            {
                if (propId == Guid.Empty || string.IsNullOrEmpty(propId.ToString()))
                {
                    throw new Exception("PropertyId is null");
                }

                var property = await _context.Properties
                                             .Where(x => x.Id == propId)
                                             .FirstOrDefaultAsync(cancellationToken);

                property.Deleted = true;

                _context.Properties.Update(property);
                await _context.SaveChangesAsync();

                return _mapper.Map<PropertyDTO>(property);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ex=", ex.Message);
                throw;
            }
        }

    }
}


