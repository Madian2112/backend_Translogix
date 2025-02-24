using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi.Common._ApiResponses;
using Academia.Translogix.WebApi.Common._BaseService;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using AutoMapper;
using Farsiman.Domain.Core.Standard.Repositories;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class ColaboradorService : BaseService<Colaboradores, ColaboradoresDto, ColaboradoresDtoInsertar, ColaboradoresDtoActualizar>
    {
        private readonly IUnitOfWork _context;

        public ColaboradorService(IMapper mapper, UnitOfWorkBuilder unitOfWork)
            : base(mapper, unitOfWork)
        {
            _context = unitOfWork.BuildDbTranslogix();
        }

        public async Task<ApiResponse<string>> GuardarColabordor(ColaboradoresDtoInsertar modeloInser)
        {
            _context.BeginTransaction();



            return ApiResponseHelper.SuccessMessage("Registro guardado con éxito");
        }
        
    }
}