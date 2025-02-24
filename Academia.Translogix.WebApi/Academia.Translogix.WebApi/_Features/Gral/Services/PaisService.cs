using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi.Common._BaseService;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using AutoMapper;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class PaisService : BaseService<Paises, PaisesDto, PaisesDtoInsertar, PaisesDtoActualizar>
    {
        public PaisService(IMapper mapper, UnitOfWorkBuilder unitOfWork)
            : base(mapper, unitOfWork)
        {
        }
    }
}
