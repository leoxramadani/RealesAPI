using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RealesApi.DTA.Intefaces.Shared;
using RealesApi.DTO.Property;
using RealesApi.Models;

namespace RealesApi.DTA.Intefaces
{
    public interface IProperty: IBaseService<Property>
	{
        Task<List<PropertyDTO>> GetAllProperties(CancellationToken cancellationToken);
        Task<PropertyDTO> GetPropertyById(Guid propId, CancellationToken cancellationToken);
        Task<PropertyDTO> CreateNewProperty(Property entity);
        Task<PropertyDTO> SoftDeleteProperty(Guid propId, CancellationToken cancellationToken);

    }
}

