using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Viaj.Requirement;
using Academia.Translogix.WebApi._Features.Viaj.Services;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using FluentAssertions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Translogix.UniTest.TestData;

namespace Translogix.UniTest
{
    public class ViajeTest
    {

        [Theory]
        [ClassData(typeof(UsuarioTestData_Nulos))]
        public void ValidadUsuarioNulo_Boolean(Usuarios usuario, bool expected)
        {
            //Arrange
            //Given: Configuramos el estado inicial y creamos las dependencias necesarias

            ViajeDominioService _viajeDominioService = new ViajeDominioService();

            //Act
            //when : Ejecutamos la accion o el metodo que queremos probar.

            bool result = _viajeDominioService.esNuloPersona(usuario);

            // Con Fluent Assertions
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(TransportistaTestData_DominioRequeriments))]
        public void ValidarDtoParaCrearTransportista_Booleano(Transportistas entidad, TransportistasDomainRequirement requirement, 
            bool expected)
        {
            //Arrange
            //Given: Configuramos el estado inicial y creamos las dependencias necesarias
            
            ViajeDominioService _viajeDominioService = new ViajeDominioService();

            //Act
            //when : Ejecutamos la accion o el metodo que queremos probar.

            bool result = _viajeDominioService.CrearTransportista(entidad, requirement).Success;

            // Con Fluent Assertions
            result.Should().Be(expected);
        }
    }
}
