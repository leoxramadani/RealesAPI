using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ResApi.DTA.Intefaces.Shared;
using ResApi.DTO.Property;
using ResApi.Models;

namespace ResApi.DTA.Intefaces
{
    public interface IProperty: IBaseService<Property>
	{
        Task<List<PropertyDTO>> GetAllProperties(CancellationToken cancellationToken);

    }
}

