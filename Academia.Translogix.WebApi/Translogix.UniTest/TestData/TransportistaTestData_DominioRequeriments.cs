using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Gral.Requirement;
using Academia.Translogix.WebApi._Features.Viaj.Requirement;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using Microsoft.Identity.Client;

namespace Translogix.UniTest.TestData
{
    public class TransportistaTestData_DominioRequeriments : TheoryData<Transportistas, TransportistasDomainRequirement, bool>
    {
       public TransportistaTestData_DominioRequeriments()
        {
            Add(TransportistaCorrecto(), RequirementCorrecto(), true);
            Add(TransportistaCorrecto(), RequirementIdentidadIgualFalse(), false);
            Add(TransportistaCorrecto(), RequirementMonedaExistenteFalse(), false);
            Add(TransportistaCorrecto(), RequirementTarifaExistenteFalse(), false);
        }

        public Transportistas TransportistaCorrecto()
            => new Transportistas
            {
                moneda_id = 1,
                persona_id = 1,
                tarifa_id = 2,
            };

        public TransportistasDomainRequirement RequirementCorrecto()
            => new TransportistasDomainRequirement
            {
                IdentidadIgual = true,
                MonedaExistente = true,
                TarifaExistente = true,
            };

        public TransportistasDomainRequirement RequirementIdentidadIgualFalse()
            => new TransportistasDomainRequirement
            {
                IdentidadIgual = false,
                MonedaExistente = true,
                TarifaExistente = true,
            };

        public TransportistasDomainRequirement RequirementMonedaExistenteFalse()
            => new TransportistasDomainRequirement
            {
                IdentidadIgual = true,
                MonedaExistente = false,
                TarifaExistente = true,
            };

        public TransportistasDomainRequirement RequirementTarifaExistenteFalse()
            => new TransportistasDomainRequirement
            {
                IdentidadIgual = true,
                MonedaExistente = true,
                TarifaExistente = false,
            };
    }
}
