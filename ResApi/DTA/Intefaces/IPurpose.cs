using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RealesApi.DTO.PurposeDTO;

namespace RealesApi.DTA.Intefaces
{
    public interface IPurpose
	{
        Task<List<PurposeDTO>> GetAllPurposes(CancellationToken cancellationToken);
    }
}

