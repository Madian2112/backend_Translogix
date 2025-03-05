using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Add(PersonaTelefonoNoNumerico(), RequirementCorrecto(), false);
            Add(PersonaTelefonoCorto(), RequirementCorrecto(), false);
            Add(PersonaCorreoInvalido(), RequirementCorrecto(), false);
            Add(PersonaCorreoLargo(), RequirementCorrecto(), false);
        }

        public Personas PersonaCorrecta()
        => new Personas
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

        public Personas PersonaIdentidadNoNumerica()
            => new Personas
            {
                identidad = "1234-567890123",
                primer_nombre = "Ana",
                segundo_nombre = "María",
                primer_apellido = "Gómez",
                segundo_apellido = "López",
                sexo = 'F',
                telefono = "99998888",
                correo_electronico = "ana.gomez@example.com",
                pais_id = 1
            };

        public Personas PersonaIdentidadCorta()
            => new Personas
            {
                identidad = "123456789", 
                primer_nombre = "Ana",
                segundo_nombre = "María",
                primer_apellido = "Gómez",
                segundo_apellido = "López",
                sexo = 'F',
                telefono = "99998888",
                correo_electronico = "ana.gomez@example.com",
                pais_id = 1
            };

        public Personas PersonaPrimerNombreLargo()
            => new Personas
            {
                identidad = "1234567890123",
                primer_nombre = "Juanitooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo",
                segundo_nombre = "María",
                primer_apellido = "Gómez",
                segundo_apellido = "López",
                sexo = 'F',
                telefono = "99998888",
                correo_electronico = "ana.gomez@example.com",
                pais_id = 1
            };

        public Personas PersonaSegundoNombreLargo()
            => new Personas
            {
                identidad = "1234567890123",
                primer_nombre = "Ana",
                primer_apellido = "Gómez",
                segundo_nombre = "Juanitooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo",
                segundo_apellido = "López",
                sexo = 'F',
                telefono = "99998888",
                correo_electronico = "ana.gomez@example.com",
                pais_id = 1
            };

        public Personas PersonaPrimerApellidoLargo()
            => new Personas
            {
                identidad = "1234567890123",
                primer_nombre = "Ana",
                segundo_nombre = "María",
                primer_apellido = "Gómezzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz", 
                segundo_apellido = "López",
                sexo = 'F',
                telefono = "99998888",
                correo_electronico = "ana.gomez@example.com",
                pais_id = 1
            };

        public Personas PersonaSegundoApellidoLargo()
            => new Personas
            {
                identidad = "1234567890123",
                primer_nombre = "Ana",
                segundo_nombre = "María",
                primer_apellido = "Gómez",
                segundo_apellido = "Lópezzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz", 
                sexo = 'F',
                telefono = "99998888",
                correo_electronico = "ana.gomez@example.com",
                pais_id = 1
            };

        public Personas PersonaSexoInvalido()
            => new Personas
            {
                identidad = "1234567890123",
                primer_nombre = "Ana",
                segundo_nombre = "María",
                primer_apellido = "Gómez",
                segundo_apellido = "López",
                sexo = 'X', // Ni 'M' ni 'F'
                telefono = "99998888",
                correo_electronico = "ana.gomez@example.com",
                pais_id = 1
            };

        
        public Personas PersonaTelefonoNoNumerico()
            => new Personas
            {
                identidad = "1234567890123",
                primer_nombre = "Ana",
                segundo_nombre = "María",
                primer_apellido = "Gómez",
                segundo_apellido = "López",
                sexo = 'F',
                telefono = "9999-8888",
                correo_electronico = "ana.gomez@example.com",
                pais_id = 1
            };

        
        public Personas PersonaTelefonoCorto()
            => new Personas
            {
                identidad = "1234567890123",
                primer_nombre = "Ana",
                segundo_nombre = "María",
                primer_apellido = "Gómez",
                segundo_apellido = "López",
                sexo = 'F',
                telefono = "1234567", 
                correo_electronico = "ana.gomez@example.com",
                pais_id = 1
            };

        
        public Personas PersonaCorreoInvalido()
            => new Personas
            {
                identidad = "1234567890123",
                primer_nombre = "Ana",
                segundo_nombre = "María",
                primer_apellido = "Gómez",
                segundo_apellido = "López",
                sexo = 'F',
                telefono = "99998888",
                correo_electronico = "ana.gomez@.com", 
                pais_id = 1
            };

        
        public Personas PersonaCorreoLargo()
            => new Personas
            {
                identidad = "1234567890123",
                primer_nombre = "Ana",
                segundo_nombre = "María",
                primer_apellido = "Gómez",
                segundo_apellido = "López",
                sexo = 'F',
                telefono = "99998888",
                correo_electronico = "ana.gomez@", 
                pais_id = 1
            };

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
