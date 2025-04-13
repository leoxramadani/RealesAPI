using System;
using RealesApi.DTO.ConditionsDTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RealesApi.DTA.Intefaces
{
	public interface IConditions
	{
        Task<List<ConditionsDTO>> GetAllConditions(CancellationToken cancellationToken);
    }
}

