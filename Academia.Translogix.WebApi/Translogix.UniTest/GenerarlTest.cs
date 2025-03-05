using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Acce.Dtos;
using Academia.Translogix.WebApi._Features.Gral.Services;
using Academia.Translogix.WebApi._Features.Viaj.Services;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Azure.Core;
using Translogix.UniTest.TestData;
using Academia.Translogix.WebApi._Features.Gral.Requirement;
using Academia.Translogix.WebApi._Features.Gral.Requeriment;

namespace Translogix.UniTest
{
    public class GenerarlTest
    {

        [Theory]
        [InlineData(0, false)]
        [InlineData(51, false)]
        [InlineData(49, true)]
        public void ValidadDistanciaSucursalCasaColaborador_RetornaBooleano(decimal distancia, bool expected)
        {
            //Arrange
            //Given: Configuramos el estado inicial y creamos las dependencias necesarias
            GeneralDominioService _generalDominioService = new GeneralDominioService();

            //Act
            //when : Ejecutamos la accion o el metodo que queremos probar.

            bool result = _generalDominioService.ValidarDistancia(distancia);

            // Con Fluent Assertions
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(ColaboradorTestData_DominioRequeriments))]
        public void ValidarDtoParaCreaColaborador_Booleano(Colaboradores entidad, ColaboradoresDomainRequirement requirement,
            bool expected)
        {
            //Arrange
            //Given: Configuramos el estado inicial y creamos las dependencias necesarias
            GeneralDominioService _generalDominioService = new GeneralDominioService();

            //Act
            //when : Ejecutamos la accion o el metodo que queremos probar.
            bool result = _generalDominioService.CrearColaborador(entidad, requirement).Success;

            // Con Fluent Assertions
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(PersonaTestData_DominioRequeriments))]
        public void ValidarDtoParaCreaPersona_Booleano(Personas entidad, PersonasDomainRequirement requirement, bool expected)
        {
            //Arrange
            //Given: Configuramos el estado inicial y creamos las dependencias necesarias
            GeneralDominioService _generalDominioService = new GeneralDominioService();

            //Act
            //when : Ejecutamos la accion o el metodo que queremos probar.

            bool result = _generalDominioService.CrearPersona(entidad, requirement).Success;

            // Con Fluent Assertions
            result.Should().Be(expected);
        }
    }
}
