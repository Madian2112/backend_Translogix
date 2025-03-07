using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Gral.Requeriment;
using Academia.Translogix.WebApi._Features.Gral.Requirement;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;

namespace Translogix.UniTest.TestData
{
    public class PersonaTestData_DominioRequeriments : TheoryData<Personas, PersonasDomainRequirement, bool>
    {
        public PersonaTestData_DominioRequeriments()
        {
            Add(PersonaCorrecta(), RequirementCorrecto(), true);
            Add(PersonaCorrecta(), RequirementPaisExitenteFalse(), false);
            Add(PersonaIdentidadNoNumerica(), RequirementCorrecto(), false);
            Add(PersonaIdentidadCorta(), RequirementCorrecto(), false);
            Add(PersonaPrimerNombreLargo(), RequirementCorrecto(), false);
            Add(PersonaSegundoNombreLargo(), RequirementCorrecto(), false);
            Add(PersonaPrimerApellidoLargo(), RequirementCorrecto(), false);
            Add(PersonaSegundoApellidoLargo(), RequirementCorrecto(), false);
            Add(PersonaSexoInvalido(), RequirementCorrecto(), false);
            Add(PersonaSexoValido(), RequirementCorrecto(), true);
            Add(PersonaTelefonoNoNumerico(), RequirementCorrecto(), false);
            Add(PersonaTelefonoCorto(), RequirementCorrecto(), false);
            Add(PersonaCorreoInvalido(), RequirementCorrecto(), false);
            Add(PersonaCorreoLargo(), RequirementCorrecto(), false);
        }

        public Personas PersonaCorrecta(Action<Personas>? configure = null)
        {
            var requirement = new Personas
            {
                identidad = "1234567890123",
                primer_nombre = "Ana",
                segundo_nombre = "María",
                primer_apellido = "Gómez",
                segundo_apellido = "López",
                sexo = 'F',
                telefono = "99998888",
                correo_electronico = "ana.gomez@example.com",
                pais_id = 1
            };

            configure?.Invoke(requirement);
            return requirement;
        }

        public Personas PersonaIdentidadNoNumerica()
            => PersonaCorrecta(x => x.identidad = "1234-567890123");

        public Personas PersonaIdentidadCorta()
            => PersonaCorrecta(x => x.identidad = "123456789");

        public Personas PersonaPrimerNombreLargo()
            => PersonaCorrecta(x => x.primer_nombre = "Madia".PadRight(71, 'n'));

        public Personas PersonaSegundoNombreLargo()
            => PersonaCorrecta(x => x.segundo_nombre = "Alejandr".PadRight(71, 'o'));

        public Personas PersonaPrimerApellidoLargo()
            => PersonaCorrecta(x => x.primer_apellido = "Reye".PadRight(71, 's'));

        public Personas PersonaSegundoApellidoLargo()
            => PersonaCorrecta(x => x.segundo_apellido = "Velasque".PadRight(71, 'z'));

        public Personas PersonaSexoInvalido()
            => PersonaCorrecta(x => x.sexo = 'X');

        public Personas PersonaSexoValido()
            => PersonaCorrecta(x => x.sexo = 'M');

        public Personas PersonaTelefonoNoNumerico()
            => PersonaCorrecta(x => x.telefono = "9999-8888");

        public Personas PersonaTelefonoCorto()
            => PersonaCorrecta(x => x.telefono = "1234567");

        public Personas PersonaCorreoInvalido()
            => PersonaCorrecta(x => x.correo_electronico = "ana.gomez@.com");

        public Personas PersonaCorreoLargo()
            => PersonaCorrecta(x => x.correo_electronico = "uanito.gomez@gmail.com".PadLeft(251, 'J'));

        public PersonasDomainRequirement RequirementCorrecto()
            => new PersonasDomainRequirement
            {
                PaisExitente = true
            };

        public PersonasDomainRequirement RequirementPaisExitenteFalse()
            => new PersonasDomainRequirement
            {
                PaisExitente = false
            };
    }
}
