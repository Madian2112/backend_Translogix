using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Data.Common;
using System.Linq;

namespace Translogix.IntegrationTests.Mocks.PaisesService
{
    public static class PaisServiceMock
    {
        // Método para registrar el mock del DbContext
        public static void AddDefaultDatabaseMock(this IServiceCollection services)
        {
            var dbContextOptions = Substitute.For<DbContextOptions<TranslogixDBContext>>();
            services.AddScoped(_ => Substitute.For<TranslogixDBContext>(dbContextOptions));
        }

        // Escenario exitoso
        public static void SetupSuccess(TranslogixDBContext dbContext)
        {
            var paisesMock = Substitute.For<DbSet<Paises>>();
            var paisList = new List<Paises>
            {
                new() { pais_id = 1, nombre = "Argentina", prefijo = 54, es_activo = true }
            }.AsQueryable();

            paisesMock.AsQueryable().Provider.Returns(paisList.Provider);
            paisesMock.AsQueryable().Expression.Returns(paisList.Expression);
            paisesMock.AsQueryable().ElementType.Returns(paisList.ElementType);
            paisesMock.AsQueryable().GetEnumerator().Returns(paisList.GetEnumerator());

            dbContext.Paises.Returns(paisesMock);
        }

        // Escenario de timeout
        public static void SetupDatabaseTimeout(TranslogixDBContext dbContext)
        {
            dbContext.Paises
                    .Returns(x => throw new InvalidOperationException("Database timeout occurred"));
        }

        // Escenario de error interno
        public static void SetupInternalServerError(TranslogixDBContext dbContext)
        {
            dbContext.Paises
                .Returns(x => throw new InvalidOperationException("Internal database server error"));
        }

        // Escenario de base de datos vacía
        public static void SetupEmptyDatabase(TranslogixDBContext dbContext)
        {
            var paisesMock = Substitute.For<DbSet<Paises>>();
            var emptyList = new List<Paises>().AsQueryable();

            paisesMock.AsQueryable().Provider.Returns(emptyList.Provider);
            paisesMock.AsQueryable().Expression.Returns(emptyList.Expression);
            paisesMock.AsQueryable().ElementType.Returns(emptyList.ElementType);
            paisesMock.AsQueryable().GetEnumerator().Returns(emptyList.GetEnumerator());

            dbContext.Paises.Returns(paisesMock);
        }
    }
}