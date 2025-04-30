using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RealesApi.DTA.Intefaces.Shared;
using RealesApi.DTO.Property;
using RealesApi.DTO.SavePropertyDTO;
using RealesApi.Models;

namespace RealesApi.DTA.Intefaces
{
    public interface IProperty: IBaseService<Property>
	{
        Task<List<PropertyDTO>> GetAllProperties(CancellationToken cancellationToken);
        Task<PropertyDTO> GetPropertyById(Guid propId, CancellationToken cancellationToken);
        Task<PropertyDTO> CreateNewProperty(PropertyDTO entity);
        Task<PropertyDTO> SoftDeleteProperty(Guid propId, CancellationToken cancellationToken);
        Task<List<PropertyDTO>> GetLatestTwoProperties(CancellationToken cancellationToken);
        Task<bool> SaveProperty(SavePropertyDTO entity);
        Task<List<PropertyDTO>> GetPropertyBySellerId(Guid sellerId, CancellationToken cancellationToken);
        int GetPropCountForSeller(Guid sellerId);
        int SavedPropertiesBySellerCount(Guid sellerId);
        Task<List<PropertyDTO>> GetPropertyForRentByUserId(Guid sellerId);
        Task<List<PropertyDTO>> GetPropertyForSaleByUserId(Guid sellerId);
        Task<List<PropertyDTO>> GetPropertiesSavedBySellerId(Guid sellerId, CancellationToken cancellationToken);
        Task<List<PropertyDTO>> GetPropertyByUserIdPending(Guid sellerId);
        Task<List<PropertyDTO>> GetPropertyByUserIdPublished(Guid sellerId);
        Task<List<PropertyDTO>> GetPropertyByUserIdRejected(Guid sellerId);
        Task<List<PropertyDTO>> SearchProperties(PropertySearchDTO search, CancellationToken cancellationToken);
    }
}

