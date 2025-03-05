using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Gral.Requirement;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;

namespace Translogix.UniTest.TestData
{
    public class PersonaTestData_Nulos : TheoryData<Personas, bool?>
    {
        public PersonaTestData_Nulos() 
        {
            Add(PersonaNula(), false);
            Add(PersonaNoNula(), true);
        }

        public Personas PersonaNula()
            => new Personas
            {
            };

        public Personas PersonaNoNula()
            => new Personas
            {
                primer_nombre = "Madian"
            };
    }
}
