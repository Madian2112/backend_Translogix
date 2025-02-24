using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Viaj.Dtos;
using Academia.Translogix.WebApi.Common._BaseService;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using AutoMapper;
namespace Academia.Translogix.WebApi._Features.Viaj.Services
{
    public class TarifaService : BaseService<Tarifas, TarifasDto, TarifasDtoInsertar, TarifasDtoActualizar>
    {
        public TarifaService(IMapper mapper, UnitOfWorkBuilder unitOfWork)
            : base(mapper, unitOfWork)
        {
            
        }
    }
}