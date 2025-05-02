using System;
using RealesApi.DTO.PropertyTypeDTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RealesApi.DTA.Intefaces
{
	public interface IPropertyType
	{
		Task<List<PropertyTypeDTO>> GetAllProperties(CancellationToken cancellationToken);
		Task<string> GetPropertyTypeById(Guid propTypeId, CancellationToken cancellationToken);
    }
}

