using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;

namespace Translogix.UniTest.TestData
{
    public class UsuarioTestData_Nulos : TheoryData<Usuarios?, bool>
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
