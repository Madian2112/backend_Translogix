using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi.Common._BaseService;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class MonedaService : BaseService<Monedas, MonedasDto, MonedasDtoInsertar, MonedasDtoActualizar>
    {
        public MonedaService(IMapper mapper, UnitOfWorkBuilder unitOfWork)
            : base(mapper, unitOfWork)
        {
        }

    }
}
