using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using AutoMapper;
using NSubstitute;

namespace Translogix.IntegrationTests.Features.Paises.Data.Scenarios
{
    public class PaisTheoryData : TheoryData<PaisesDtoInsertar, int>
    {
        public PaisTheoryData()
        {
            // Caso válido
            Add(PaisCorrecto(), 200);

            // Caso inválido: Nombre nulo
        }

        //public IMapper ConfigureMapperMockPaises()
        //{
        //    var mapperMock = Substitute.For<IMapper>();
        //    mapperMock.Map<Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral.Paises>(Arg.Any<PaisesDtoInsertar>()).Returns(x =>
        //    {
        //        var input = x.Arg<PaisesDtoInsertar>();
        //        return new Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral.Paises       
        //        {
        //            pais_id = 1,
        //            nombre = input?.nombre ?? "Default",
        //            usuario_creacion = 1,
        //            fecha_creacion = DateTime.UtcNow,
        //            es_activo = true,
        //            UsuarioCrear = new Usuarios { usuario_id = 1, nombre = "TestUser" }
        //        };
        //    });
        //    return mapperMock;
        //}

        public PaisesDtoInsertar PaisCorrecto(Action<PaisesDtoInsertar>? configure = null)
        {
            var paises = new PaisesDtoInsertar
            {
                nombre = "Bulgaria",
                prefijo = 88,
                es_activo = true,
                usuario_creacion = 1,
                fecha_creacion = DateTime.UtcNow
            };

            configure?.Invoke(paises);
            return paises;
        }
    }
}
