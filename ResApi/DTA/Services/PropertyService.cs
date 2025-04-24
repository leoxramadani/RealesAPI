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
using RealesApi.DTO.SavePropertyDTO;
using RealesApi.DTO.WhatsSpecialDTO;
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
                                           .OrderByDescending(x => x.DatePosted)
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
        //Get three latest properties
        public async Task<List<PropertyDTO>> GetLatestTwoProperties(CancellationToken cancellationToken)
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
                                           .OrderByDescending(x => x.DatePosted)
                                           .Select(x => _mapper.Map<PropertyDTO>(x))
                                           .Take(2)
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
        //Get all properties by seller Id
        public async Task<List<PropertyDTO>> GetPropertyBySellerId(Guid sellerId, CancellationToken cancellationToken)
        {
            try
            {
                if (sellerId == Guid.Empty || string.IsNullOrEmpty(sellerId.ToString()))
                {
                    throw new Exception("SellerId is null");
                }


                var property = await _context.Properties
                                          .Include(x => x.PropertyOtherImages)
                                           .Include(x => x.Condition)
                                           .Include(x => x.Users)
                                           .Include(x => x.PropertyWhatsSpecialLinks)
                                           .Include(x => x.PropertyType)
                                           .Where(x => x.SellerId == sellerId && x.Deleted != true)
                                           .Select(x => _mapper.Map<PropertyDTO>(x))
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

        //Get properties by seller Id for rent 
        public async Task<List<PropertyDTO>> GetPropertyBySellerIdForRent(Guid sellerId, CancellationToken cancellationToken)
        {
            try
            {
                if (sellerId == Guid.Empty || string.IsNullOrEmpty(sellerId.ToString()))
                {
                    throw new Exception("SellerId is null");
                }


                var property = await _context.Properties .Include(x => x.Users)
                                                         .Include(x => x.PropertyType)
                                                         .Include(x => x.Purpose)
                                                         .Where(x => x.Deleted != true && x.SellerId == sellerId)
                                                         .OrderByDescending(x => x.DatePosted)
                                                         .Select(x => _mapper.Map<PropertyDTO>(x))
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
        public async Task<List<PropertyDTO>> GetPropertyForRentByUserId(Guid sellerId)
        {
            try
            {
                if (sellerId == Guid.Empty || string.IsNullOrEmpty(sellerId.ToString()))
                {
                    throw new Exception("SellerId is null");
                }

                var properties = await _context.Properties.Include(x => x.Users)
                                                          .Include(x => x.PropertyType)
                                                          .Include(x => x.Purpose)
                                                          .Where(x => x.Deleted != true && x.SellerId == sellerId && x.Purpose.Name == "Rent")
                                                          .Select(x => _mapper.Map<PropertyDTO>(x))
                                                          .ToListAsync();
                if (properties == null)
                    return null;

                return properties;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ex=", ex.Message);
                throw;
            }
        }
        public async Task<List<PropertyDTO>> GetPropertyForSaleByUserId(Guid sellerId)
        {
            try
            {
                var properties = await _context.Properties.Include(x => x.Users)
                                                         .Include(x => x.PropertyType)
                                                         .Include(x => x.Purpose)
                                                         .Where(x => x.Deleted != true && x.SellerId == sellerId && x.Purpose.Name == "Sale")
                                                         .Select(x => _mapper.Map<PropertyDTO>(x))
                                                         .ToListAsync();
                if (properties == null)
                    return null;

                return properties;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ex=", ex.Message);
                throw;
            }
        }

        public int GetPropCountForSeller(Guid sellerId)
        {
            try
            {
                if (sellerId == Guid.Empty || string.IsNullOrEmpty(sellerId.ToString()))
                {
                    throw new Exception("SellerId is null");
                }

                var countProps = _context.Properties.Where(x => x.SellerId == sellerId && x.Deleted != true).Count();

                return countProps;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ex=", ex.Message);
                throw;
            }
        }
        public async Task<bool> SaveProperty(SavePropertyDTO entity)
        {
            try
            {
                if (entity == null)
                    return false;

                var checkIfAlreadySaved = _context.SaveProperties.Any(x => x.PropertyId == entity.PropertyId && x.SellerId == entity.SellerId);
                if (checkIfAlreadySaved)
                    return false;

                if (!checkIfAlreadySaved)
                {
                    SaveProperty sp = new()
                    {
                        PropertyId = entity.PropertyId,
                        SellerId = entity.SellerId,
                        CreatedAt = DateTime.Now,
                        CreatedBy = "Admin",
                        Deleted = false,
                        ModifiedAt = DateTime.Now,
                        ModifiedBy = "Admin"
                    };

                    _context.SaveProperties.Add(sp);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ex=", ex.Message);
                throw;
            }
        }
        public int SavedPropertiesBySellerCount(Guid sellerId)
        {
            try
            {
                if (sellerId == Guid.Empty || string.IsNullOrEmpty(sellerId.ToString()))
                {
                    throw new Exception("SellerId is null");
                }

                var countProps = _context.SaveProperties
                                         .Include(x=>x.Property)
                                         .Where(x => x.SellerId == sellerId && x.Property.Deleted == false)
                                         .Count();

                return countProps;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ex=", ex.Message);
                throw;
            }
        }

        public async Task<List<PropertyDTO>> GetPropertiesSavedBySellerId(Guid sellerId,CancellationToken cancellationToken)
        {
            try
            {
                if (sellerId == Guid.Empty || string.IsNullOrEmpty(sellerId.ToString()))
                {
                    throw new Exception("SellerId is null");
                }

                var savedProps = await _context.SaveProperties
                                                .Include(x => x.Property)
                                                .ThenInclude(p => p.PropertyType)
                                                .Include(x => x.Property)
                                                .ThenInclude(p => p.Purpose)
                                                .Include(x => x.Property)
                                                .ThenInclude(p => p.Condition)
                                                .Include(x => x.Users)
                                                .Where(x => x.SellerId == sellerId && x.Property.Deleted == false)
                                                .Select(x => new PropertyDTO
                                                {
                                                    Id = x.Property.Id,
                                                    Name = x.Property.Name,
                                                    ReviewsRates = x.Property.ReviewsRates ?? Guid.Empty,
                                                    Views = x.Property.Views ?? 0,
                                                    DatePosted = x.Property.DatePosted ?? DateTime.MinValue,
                                                    Saves = x.Property.Saves ?? 0,
                                                    MainImage = x.Property.MainImage,
                                                    Price = x.Property.Price,
                                                    Rooms = x.Property.Rooms ?? 0,
                                                    Baths = x.Property.Baths ?? 0,
                                                    Size = x.Property.Size ?? 0,
                                                    BuiltIn = x.Property.BuiltIn,
                                                    AllDocuments = x.Property.AllDocuments ?? false,
                                                    MonthlyPayment = x.Property.MonthlyPayment ?? false,
                                                    ConditionName = x.Property.Condition.Name,
                                                    ConditionId = x.Property.ConditionId,
                                                    PropertyTypeId = x.Property.PropertyTypeId,
                                                    PropertyTypeName = x.Property.PropertyType.Name,
                                                    PurposeId = x.Property.PurposeId,
                                                    PurposeName = x.Property.Purpose.Name,
                                                    UserId = x.Property.Users.Id,
                                                    UserName = x.Property.Users.Name,
                                                    UserLastName = x.Property.Users.LastName,
                                                    UserEmail = x.Property.Users.Email,
                                                    UserPhone = x.Property.Users.PhoneNumber,
                                                    SellerId = x.Property.SellerId,
                                                    Description = x.Property.Description,
                                                    Location = x.Property.Location,
                                                    PriceRange = x.Property.PriceRange,
                                                    CreatedBy = x.Property.CreatedBy,
                                                    CreatedAt = (DateTime)x.Property.CreatedAt,
                                                    ModifiedBy = x.Property.ModifiedBy,
                                                    ModifiedAt = (DateTime)x.Property.ModifiedAt,
                                                    Deleted = x.Property.Deleted,
                                                    })
                                                    .ToListAsync(cancellationToken);
                return savedProps;
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
                    DatePosted = DateTime.Now,

                    AllDocuments = entity.AllDocuments,
                    Baths = entity.Baths,
                    BuiltIn = entity.BuiltIn,
                    ConditionId = entity.ConditionId,
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

                if(entity.WhatsSpecialNames.Count > 0)
                {

                    foreach (var item in entity.WhatsSpecialNames)
                    {
                        PropertyWhatsSpecialLink pwsl = new()
                        {
                            PropertyId = prop.Id,
                            WhatsSpecialId = item.Id,
                        };
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


