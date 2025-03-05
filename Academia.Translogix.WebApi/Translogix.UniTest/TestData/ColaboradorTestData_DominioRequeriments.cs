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

        }
        public Colaboradores ColaboradorCorrecto()
            => new Colaboradores
            {
                cargo_id = 1,
                estado_civil_id = 2,
                fecha_nacimiento = DateTime.Now,
                latitud = (decimal)215.005,
                longitud = (decimal)22.11,
            };

        public Colaboradores ColaboradorLatitudCero()
        => new Colaboradores
        {
            cargo_id = 1,
            estado_civil_id = 2,
            fecha_nacimiento = DateTime.ParseExact("31/12/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            latitud = 0,
            longitud = (decimal)215.15
        };

        public Colaboradores ColaboradorLatitudFormatoInvalido()
        => new Colaboradores
        {
            cargo_id = 1,
            estado_civil_id = 2,
            fecha_nacimiento = DateTime.ParseExact("31/12/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            latitud = (decimal)12345.1234567890123456,
            longitud = (decimal)215.15
        };

        public Colaboradores ColaboradorLongitudCero()
        => new Colaboradores
        {
            cargo_id = 1,
            estado_civil_id = 2,
            fecha_nacimiento = DateTime.ParseExact("31/12/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            latitud = (decimal)215.15,
            longitud = 0
        };

        public ColaboradoresDomainRequirement RequirementCorrecto()
            => new ColaboradoresDomainRequirement
            {
                CargoExistente = true,
                CorreoIgual = true, 
                EstadoCivilExistente = true,
                IdentidadIgual = true
            };

        public ColaboradoresDomainRequirement RequirementCargoExistenteFalse()
            => new ColaboradoresDomainRequirement
            {
                CargoExistente = false,
                CorreoIgual = true,
                EstadoCivilExistente = true,
                IdentidadIgual = true
            };

        public ColaboradoresDomainRequirement RequirementCorreoIgualFalse()
            => new ColaboradoresDomainRequirement
            {
                CargoExistente = true,
                CorreoIgual = false,
                EstadoCivilExistente = true,
                IdentidadIgual = true
            };

        public ColaboradoresDomainRequirement RequirementEstadoCivilExistenteFalse()
            => new ColaboradoresDomainRequirement
            {
                CargoExistente = true,
                CorreoIgual = true,
                EstadoCivilExistente = false,
                IdentidadIgual = true
            };

        public ColaboradoresDomainRequirement RequirementIdentidadIgualFalse()
            => new ColaboradoresDomainRequirement
            {
                CargoExistente = true,
                CorreoIgual = true,
                EstadoCivilExistente = true,
                IdentidadIgual = false
            };
    }
}
