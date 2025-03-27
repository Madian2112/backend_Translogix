using Academia.Translogix.WebApi._Features.Acce.Dtos;
using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi._Features.Viaj.Dtos;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using AutoMapper;
using static Academia.Translogix.WebApi._Features.Viaj.Dtos.TransportistasDto;
using static Academia.Translogix.WebApi._Features.Viaj.Dtos.ViajesDetallesDto;

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

            CreateMap<Usuarios, UsuariosDto>()
                .ForMember(u => u.rol, dto => dto.MapFrom(rol => rol.Rol.nombre))
                .ForMember(u => u.cargo, dto => dto.MapFrom(cargo => cargo.Colaborador.Cargo.nombre))
                .ForMember(u => u.Persona, dto => dto.MapFrom(src => src.Colaborador.Persona))
                .ForMember(u => u.Colaborador, dto => dto.MapFrom(src => src.Colaborador))
                .ReverseMap();

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

            CreateMap<Colaboradores, ColaboradoresDto>()
                .ForMember(c => c.Persona, dto => dto.MapFrom(p => p.Persona))
                .ForMember(c => c.Cargo, dto => dto.MapFrom(carg => carg.Cargo))
                .ForMember(c => c.EstadoCivil, dto => dto.MapFrom(estc => estc.EstadoCivil))
                .ReverseMap();
            CreateMap<Colaboradores, ColaboradoresInsertar>().ReverseMap();
            CreateMap<Colaboradores, ColaboradoresDtoActualizar>().ReverseMap();

            CreateMap<Transportistas, TransportistaDto>()
                .ForMember(t => t.Tarifa, dto => dto.MapFrom(t => t.Tarifa))
                .ForMember(p => p.Persona, dto => dto.MapFrom(p => p.Persona));
            CreateMap<Transportistas, TransportistaInsertar>().ReverseMap();

            CreateMap<Personas, PersonasDto>().ReverseMap();
            CreateMap<Personas, PersonasDtoInsertar>().ReverseMap();

            CreateMap<EstadosCiviles, EstadosCivilesDto>().ReverseMap();
            CreateMap<EstadosCiviles, EstadosCivilesDtoInsertar>().ReverseMap();
            CreateMap<EstadosCiviles, EstadosCivilesDtoActualizar>().ReverseMap();

            CreateMap<Viajes, ViajesInsertarDto>().ReverseMap();

            CreateMap<Viajes, ViajesDto>()
    .ForMember(dest => dest.sucursal, opt => opt.MapFrom(src => src.Sucursal))
    .ForMember(dest => dest.transportista, opt => opt.MapFrom(src => src.Transportista.Persona));

            CreateMap<ViajesDto, Viajes>()
                .ForMember(dest => dest.Sucursal, opt => opt.MapFrom(src => src.sucursal))
                .ForMember(dest => dest.Transportista, opt => opt.Ignore());

            CreateMap<ViajesDetalles, ViajesDetalleInsertarDto>().ReverseMap();

            CreateMap<SucursalesColaboradores, SucursalesColaboradoresDto>().ReverseMap();
            CreateMap<SucursalesColaboradores, SucursalesColaboradoresInsertar>().ReverseMap();
            CreateMap<SucursalesColaboradores, SucursalesColaboradoresActualizarDto>().ReverseMap();
        }
    }
}
