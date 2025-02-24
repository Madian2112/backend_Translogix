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
using Farsiman.Domain.Core.Standard;
using Farsiman.Domain.Core.Standard.Repositories;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class CargoService : BaseService<Cargos, CargosDto, CargosDtoInsertar, CargosDtoActualizar>
    {
        public CargoService(IMapper mapper, UnitOfWorkBuilder unitOfWork) 
            : base(mapper, unitOfWork)
        {
            
        }
    }

}