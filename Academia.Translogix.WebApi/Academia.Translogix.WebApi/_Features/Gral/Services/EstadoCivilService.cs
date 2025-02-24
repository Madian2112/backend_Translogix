using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi.Common._BaseService;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using AutoMapper;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class EstadoCivilService : BaseService<Estados_Civiles, EstadosCivilesDto, EstadosCivilesDtoInsertar, EstadosCivilesDtoActualizar>
    {
        public EstadoCivilService(IMapper mapper, UnitOfWorkBuilder unitOfWork)
            : base(mapper, unitOfWork)
        {
        }
    }
}