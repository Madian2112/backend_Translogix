using Academia.Translogix.WebApi._Features.Acce.Requirement;
using Academia.Translogix.WebApi._Features.Acce.Services;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using FluentAssertions;
using Translogix.UniTest.TestData;

namespace Translogix.UniTest
{
    public class AccesoTest
    {
        [Theory]
        [ClassData(typeof(UsuarioTestData_DominioRequeriments))]
        public void ValidarDtoParaCreaUsuario_Booleano(Usuarios entidad, UsuariosDomainRequirement requirement,
            bool expected)
        {
            //Arrange
            //Given: Configuramos el estado inicial y creamos las dependencias necesarias
            AccesoDominioService _accesoDominioService = new AccesoDominioService();

            //Act
            //when : Ejecutamos la accion o el metodo que queremos probar.
            bool result = _accesoDominioService.CrearUsuario(entidad, requirement).Success;

            // Con Fluent Assertions
            result.Should().Be(expected);
        }
    }
}
