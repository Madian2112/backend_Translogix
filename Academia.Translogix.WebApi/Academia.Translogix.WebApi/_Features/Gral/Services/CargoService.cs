using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi.Infrastructure._BaseService;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using AutoMapper;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class CargoService : BaseService<Cargos, CargosDto, CargosDtoInsertar, CargosDtoActualizar>
    {
        public CargoService(TranslogixDBContext context, IMapper mapper) 
            : base(context, mapper)
        {
            
        }
    }

}