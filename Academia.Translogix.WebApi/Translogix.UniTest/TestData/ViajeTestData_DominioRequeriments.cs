using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Gral.Requirement;
using Academia.Translogix.WebApi._Features.Viaj.Requirement;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;

namespace Translogix.UniTest.TestData
{
    public class ViajeTestData_DominioRequeriments : TheoryData<Viajes, ViajesDomainRequirement, bool>
    {
        public ViajeTestData_DominioRequeriments()
        {
            Add(ViajeCorrecto(), RequirementCorrecto(), true);
            Add(ViajeCorrecto(), RequirementSucursalExistenteFalse(), false);
            Add(ViajeCorrecto(), RequirementTransportistaExistenteFalse(), false);
        }

        public Viajes ViajeCorrecto(Action<Viajes>? configure = null)
        {
            var requirement = new Viajes
            {
                fecha = DateTime.UtcNow,
                distancia_recorrida_km = 100,
                total_pagar = 1000,
                sucursal_id = 1,
                transportista_id = 1,
                usuario_creacion = 3,
                fecha_creacion = DateTime.UtcNow,

            };
            configure?.Invoke(requirement);
            return requirement;
        }

        public ViajesDomainRequirement RequirementCorrecto()
            => new ViajesDomainRequirement
            {
                SucursalExistente = true,
                TransportistaExistente = true
            };

        public ViajesDomainRequirement RequirementSucursalExistenteFalse()
            => new ViajesDomainRequirement
            {
                SucursalExistente = false,
                TransportistaExistente = true
            };

        public ViajesDomainRequirement RequirementTransportistaExistenteFalse()
            => new ViajesDomainRequirement
            {
                SucursalExistente = true,
                TransportistaExistente = false
            };
    }
}
