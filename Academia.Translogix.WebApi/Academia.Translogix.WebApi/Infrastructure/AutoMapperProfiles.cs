using Academia.Translogix.WebApi._Features.Acce.Dtos;
using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi._Features.Viaj.Dtos;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using AutoMapper;

namespace Academia.Translogix.WebApi.Infrastructure
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Monedas, MonedasDto>().ReverseMap();
            CreateMap<Monedas, MonedasDtoInsertar>().ReverseMap();
            CreateMap<Monedas, MonedasDtoActualizar>().ReverseMap();

            CreateMap<Cargos, CargosDto>().ReverseMap();
            CreateMap<Cargos, CargosDtoInsertar>().ReverseMap();
            CreateMap<Cargos, CargosDtoActualizar>().ReverseMap();

            CreateMap<Paises, PaisesDto>().ReverseMap();
            CreateMap<Paises, PaisesDtoInsertar>().ReverseMap();
            CreateMap<Paises, PaisesDtoActualizar>().ReverseMap();

            CreateMap<Usuarios, UsuariosDto>().ReverseMap();
            CreateMap<Usuarios, UsuariosDtoInsertar>().ReverseMap();
            CreateMap<Usuarios, UsuariosDtoActualizar>().ReverseMap();

            CreateMap<Roles, RolesDto>().ReverseMap();
            CreateMap<Roles, RolesDtoInsertar>().ReverseMap();
            CreateMap<Roles, RolesDtoActualizar>().ReverseMap();

            CreateMap<Tarifas, TarifasDto>().ReverseMap();
            CreateMap<Tarifas, TarifasDtoInsertar>().ReverseMap();
            CreateMap<Tarifas, TarifasDtoActualizar>().ReverseMap();

            CreateMap<Sucursales, SucursalesDto>().ReverseMap();
            CreateMap<Sucursales, SucursalesDtoInsertar>().ReverseMap();
            CreateMap<Sucursales, SucursalesDtoActualizar>().ReverseMap();

            CreateMap<Colaboradores, ColaboradoresDto>().ReverseMap();
            CreateMap<Colaboradores, ColaboradoresDtoInsertar>().ReverseMap();
            CreateMap<Colaboradores, ColaboradoresDtoActualizar>().ReverseMap();

            CreateMap<Estados_Civiles, EstadosCivilesDto>().ReverseMap();
            CreateMap<Estados_Civiles, EstadosCivilesDtoInsertar>().ReverseMap();
            CreateMap<Estados_Civiles, EstadosCivilesDtoActualizar>().ReverseMap();

            CreateMap<Sucursales_Colaboradores, SucursalesColaboradoresDto>().ReverseMap();
            CreateMap<Sucursales_Colaboradores, SucursalesColaboradoresInsertarDto>().ReverseMap();
            CreateMap<Sucursales_Colaboradores, SucursalesColaboradoresActualizarDto>().ReverseMap();
        }
    }
}
