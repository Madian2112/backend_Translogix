using System;
using System.Data.Common;
using System.Net;
using System.Net.Http.Json;
using Academia.Translogix.WebApi._Features.Acce.Dtos;
using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi.Common;
using Academia.Translogix.WebApi.Common._ApiResponses;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Translogix.IntegrationTests.Features.Paises.Data.Scenarios;
using Translogix.IntegrationTests.Helpers;
using Translogix.IntegrationTests.Mocks.PaisesService;

namespace Translogix.IntegrationTests.Features.Paises
{
    public class PaisControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        //private readonly CustomWebApplicationFactory<Program> _factory;
        private const string baseUrl = "/api/Pais";

        public PaisControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Dado_QueSeHanSembradoPaises_CuandoSeInvocaElEndpointDePaises_Entonces_Retorna200YUnaColeccionDePaisesNoNula()
        {
            try
            {
                var paises = new PaisesDtoInsertar
                {
                    nombre = "Bulgaria",
                    prefijo = 88,
                    es_activo = true,
                    usuario_creacion = 1,
                    fecha_creacion = DateTime.UtcNow
                };

                var guardarPais =  _httpClient.PostAsJsonAsync($"{baseUrl}/InsertarPais", paises).Result;
                var responseGuardar = await guardarPais.Content.ReadFromJsonAsync<ApiResponse<string>>();
                responseGuardar.StatusCode.Should().Be(200);

                var response = await _httpClient.GetAsync($"{baseUrl}/ObtenerPaises");
                // Asserts
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<PaisesDto>>>();
                apiResponse.StatusCode.Should().Be(200);
                apiResponse.Should().NotBeNull();
                apiResponse.Data.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw; 
            }
        }

        [Fact]
        public async Task Cuando_LaBaseDeDatosTieneTimeout_Entonces_Retorna500()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<TranslogixDBContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "TestDatabase"); // Usar InMemory para simplificar
            var dbContextOptions = optionsBuilder.Options;

            var dbContextMock = Substitute.For<TranslogixDBContext>(dbContextOptions);
            PaisServiceMock.SetupDatabaseTimeout(dbContextMock);

            // Configurar Database para evitar problemas
            dbContextMock.Database.Returns(Substitute.For<Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade>(dbContextMock));

            var factoryWithMock = new CustomWebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.RemoveAll(d => d.ServiceType == typeof(TranslogixDBContext));
                        services.AddScoped(_ => dbContextMock);
                    });
                });

            var client = factoryWithMock.CreateClient();

            // Act
            var response = await client.GetAsync($"{baseUrl}/ObtenerPaises");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<PaisesDto>>>();
            apiResponse.Should().NotBeNull();
            apiResponse!.Success.Should().BeFalse();
            apiResponse!.Message.Should().Contain("timeout");
            apiResponse!.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Cuando_LaBaseDeDatosFallaInternamente_Entonces_Retorna500()
        {
            // Arrange
            var optionsBuilder = new DbContextOptionsBuilder<TranslogixDBContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "TestDatabase");
            var dbContextOptions = optionsBuilder.Options;

            var dbContextMock = Substitute.For<TranslogixDBContext>(dbContextOptions);
            PaisServiceMock.SetupInternalServerError(dbContextMock);

            // Configurar Database para evitar problemas
            dbContextMock.Database.Returns(Substitute.For<Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade>(dbContextMock));

            var factoryWithMock = new CustomWebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.RemoveAll(d => d.ServiceType == typeof(TranslogixDBContext));
                        services.AddScoped(_ => dbContextMock);
                    });
                });

            var client = factoryWithMock.CreateClient();

            // Act
            var response = await client.GetAsync($"{baseUrl}/ObtenerPaises");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<PaisesDto>>>();
            apiResponse.Should().NotBeNull();
            apiResponse!.Success.Should().BeFalse();
            apiResponse!.Message.Should().Contain("Internal database server error");
            apiResponse!.StatusCode.Should().Be(500);
        }

    }
}