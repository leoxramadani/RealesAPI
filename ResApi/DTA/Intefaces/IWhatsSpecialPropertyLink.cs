using System;
using RealesApi.DTO.WhatsSpecialDTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RealesApi.DTA.Intefaces
{
	public interface IWhatsSpecialPropertyLink
	{
        Task<List<WhatsSpecialLinkDTO>> GetAllSpecialsLinks(CancellationToken cancellationToken);
        Task<List<WhatsSpecialLinkDTO>> GetSpecialsById(Guid propId, CancellationToken cancellationToken);
        Task<WhatsSpecialDTO> CreateWhatsSpecial(WhatsSpecialDTO entity);
        Task<List<WhatsSpecialDTO>> GetAllSpecials(CancellationToken cancellationToken);
    }
}

