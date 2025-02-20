using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi.Infrastructure._ApiResponses;
using Academia.Translogix.WebApi.Infrastructure._BaseService;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using AutoMapper;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class PaisService : BaseService<Paises, PaisesDto, PaisesDtoInsertar, PaisesDtoActualizar>
    {
        public PaisService(TranslogixDBContext translogix, IMapper mapper) 
            : base(translogix, mapper)
        {
        }
    }
}
