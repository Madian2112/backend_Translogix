using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;

namespace Translogix.UniTest.TestData
{
    public class TransportistaTestData_Nulos : TheoryData<Transportistas, bool>
    {
        public TransportistaTestData_Nulos() 
        {
            var transportistaMock = new Transportistas
            {
                transportista_id = 1
            };

            Add(null, false);
            Add(transportistaMock, true);
        }
    }
}
