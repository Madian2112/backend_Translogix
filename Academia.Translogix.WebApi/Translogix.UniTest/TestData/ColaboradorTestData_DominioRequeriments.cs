using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Gral.Requeriment;
using Academia.Translogix.WebApi._Features.Gral.Requirement;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;

namespace Translogix.UniTest.TestData
{
    public class ColaboradorTestData_DominioRequeriments : TheoryData<Colaboradores, ColaboradoresDomainRequirement, bool>
    {

        public ColaboradorTestData_DominioRequeriments()
        {
            Add(ColaboradorCorrecto(), RequirementCorrecto(), true);
            Add(ColaboradorCorrecto(), RequirementCargoExistenteFalse(), false);
            Add(ColaboradorCorrecto(), RequirementCorreoIgualFalse(), false);
            Add(ColaboradorCorrecto(), RequirementEstadoCivilExistenteFalse(), false);
            Add(ColaboradorCorrecto(), RequirementIdentidadIgualFalse(), false);
            Add(ColaboradorLatitudCero(), RequirementCorrecto(), false);
            Add(ColaboradorLatitudFormatoInvalido(), RequirementCorrecto(), false);
            Add(ColaboradorLongitudCero(), RequirementCorrecto(), false);
            Add(ColaboradorFechaValida(), RequirementCorrecto(), true);

        }
        public Colaboradores ColaboradorCorrecto(Action<Colaboradores>? configure = null)
        {
            var colaborador = new Colaboradores
            {
                cargo_id = 1,
                estado_civil_id = 2,
                fecha_nacimiento = DateTime.Now,
                latitud = (decimal)215.005,
                longitud = (decimal)22.11
            };
            configure?.Invoke(colaborador);
            return colaborador;
        }

        public Colaboradores ColaboradorLatitudCero()
            => ColaboradorCorrecto(x => x.latitud = 0);

        public Colaboradores ColaboradorLatitudFormatoInvalido()
        => ColaboradorCorrecto(x => x.latitud = (decimal)12345.1234567890123456);

        public Colaboradores ColaboradorLongitudCero()
        => ColaboradorCorrecto(x => x.longitud = 0);

        public Colaboradores ColaboradorFechaValida()
            => ColaboradorCorrecto(x => x.fecha_nacimiento = DateTime.Parse("31-12-2023"));

        public ColaboradoresDomainRequirement RequirementCorrecto(Action<ColaboradoresDomainRequirement>? configure = null)
        {
            var requirement = new ColaboradoresDomainRequirement
            {
                CargoExistente = true,
                CorreoIgual = true,
                EstadoCivilExistente = true,
                IdentidadIgual = true
            };

            configure?.Invoke(requirement);
            return requirement;
        }

        public ColaboradoresDomainRequirement RequirementCargoExistenteFalse()
            => RequirementCorrecto(x => x.CargoExistente = false);

        public ColaboradoresDomainRequirement RequirementCorreoIgualFalse()
            => RequirementCorrecto(x => x.CorreoIgual = false);

        public ColaboradoresDomainRequirement RequirementEstadoCivilExistenteFalse()
            => RequirementCorrecto(x => x.EstadoCivilExistente = false);

        public ColaboradoresDomainRequirement RequirementIdentidadIgualFalse()
            => RequirementCorrecto(x => x.IdentidadIgual = false);
    }
}
