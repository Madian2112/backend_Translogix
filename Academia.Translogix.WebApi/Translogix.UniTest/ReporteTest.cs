using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Reportes.Service;

namespace Translogix.UniTest
{
    public class ReporteTest
    {
        private readonly ReporteDominioService _reporteDominioService;
        public ReporteTest()
        {
            _reporteDominioService = new ReporteDominioService();
        }

        [Theory]
        [InlineData("01/01/2021", "31/01/2021", true)]
        [InlineData("01/06/2023", "31/05/2023", false)]
        public void ValidarFechaInicioMenorFechaFin_RetornaBooleano(string FechaInicio, string FechaFin, bool expected)
        {
            //Arrange
            //Given: Configuramos el estado inicial y creamos las dependencias necesarias

            //Act
            //when : Ejecutamos la accion o el metodo que queremos probar.
            var result = _reporteDominioService.ValidarFechas(DateTime.Parse(FechaInicio), DateTime.Parse(FechaFin));

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result, expected);
        }
    }
}
