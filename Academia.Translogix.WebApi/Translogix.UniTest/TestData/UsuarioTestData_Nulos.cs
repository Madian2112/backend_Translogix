using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Microsoft.Identity.Client;

namespace Translogix.UniTest.TestData
{
    public class UsuarioTestData_Nulos : TheoryData<Usuarios, bool>
    {
        public UsuarioTestData_Nulos() 
        {
            var usuariosMock = new Usuarios
            {
                nombre = "Madian"
            };

            Add(null, true);
            Add(usuariosMock, false);
        }
    }
}
