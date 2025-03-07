using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using AutoMapper;
using Farsiman.Domain.Core.Standard.Repositories;
using Farsiman.Infraestructure.Core.Entity.Standard;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;

namespace Translogix.IntegrationTests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                // Eliminar el DbContext original si existe
                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TranslogixDBContext>));
                if (dbContextDescriptor != null)
                {
                    services.Remove(dbContextDescriptor);
                }
                services.AddSingleton<IHostEnvironment>(sp => new TestHostEnvironment { EnvironmentName = "test" });

                // Eliminar el UnitOfWorkBuilder original si existe
                var unitOfWorkDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(UnitOfWorkBuilder));
                if (unitOfWorkDescriptor != null)
                {
                    services.Remove(unitOfWorkDescriptor);
                }

                SQLitePCL.Batteries_V2.Init();
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();

                // Registrar DbContext con SQLite en memoria
                services.AddDbContext<TranslogixDBContext>(options =>
                {
                    options.UseSqlite(connection);
                }, ServiceLifetime.Scoped);

                // Verificar que el contexto se pueda resolver
                using (var scope = services.BuildServiceProvider().CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<TranslogixDBContext>();
                    if (db == null)
                    {
                        throw new InvalidOperationException("No se pudo resolver TranslogixDBContext.");
                    }
                    db.Database.EnsureCreated();
                    db.Database.ExecuteSqlRaw("PRAGMA foreign_keys = OFF;");
                }


                services.AddScoped<UnitOfWorkBuilder>();
                services.AddScoped<IUnitOfWork>(serviceProvider =>
                {
                    var dbContext = serviceProvider.GetRequiredService<TranslogixDBContext>();
                    return new UnitOfWork(dbContext);
                });
            });

            builder.UseEnvironment("test");
        }
    }
}