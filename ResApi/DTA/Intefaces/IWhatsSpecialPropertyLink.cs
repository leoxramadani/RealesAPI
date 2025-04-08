using System;
using RealesApi.DTO.WhatsSpecialDTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RealesApi.DTA.Intefaces
{
	public interface IWhatsSpecialPropertyLink
	{
        Task<List<WhatsSpecialLinkDTO>> GetAllSpecials(CancellationToken cancellationToken);
        Task<List<WhatsSpecialLinkDTO>> GetSpecialsById(Guid propId, CancellationToken cancellationToken);
    }
}

