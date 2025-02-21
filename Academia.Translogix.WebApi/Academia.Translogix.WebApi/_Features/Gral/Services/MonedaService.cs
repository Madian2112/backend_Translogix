using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi.Infrastructure._BaseService;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class MonedaService : BaseService<Monedas, MonedasDto, MonedasDtoInsertar, MonedasDtoActualizar>
    {
        public MonedaService(TranslogixDBContext translogixDBContext, IMapper mapper)
            : base(translogixDBContext, mapper)
        {
        }

    }
}
