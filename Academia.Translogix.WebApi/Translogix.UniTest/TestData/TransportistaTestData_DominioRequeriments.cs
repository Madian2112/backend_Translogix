using Academia.Translogix.WebApi._Features.Viaj.Requirement;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;

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

        public TransportistasDomainRequirement RequirementCorrecto(Action<TransportistasDomainRequirement>? configure = null)
        {
            var requirement = new TransportistasDomainRequirement
            {
                IdentidadIgual = true,
                MonedaExistente = true,
                TarifaExistente = true,
            };

            configure?.Invoke(requirement);
            return requirement;
        }

        public TransportistasDomainRequirement RequirementIdentidadIgualFalse()
            => RequirementCorrecto(x => x.IdentidadIgual = false);

        public TransportistasDomainRequirement RequirementMonedaExistenteFalse()
            => RequirementCorrecto(x => x.MonedaExistente = false);

        public TransportistasDomainRequirement RequirementTarifaExistenteFalse()
            => RequirementCorrecto(x => x.TarifaExistente = false);
    }
}
