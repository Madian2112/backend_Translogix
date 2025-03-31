using Academia.Translogix.WebApi.Common;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Farsiman.Domain.Core.Standard.Repositories;
using Farsiman.Extensions.Configuration;
using Farsiman.Infraestructure.Core.Entity.Standard;
using Microsoft.EntityFrameworkCore;

namespace Academia.Translogix.WebApi;
public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                corsBuilder =>
                {
                    if (builder.Environment.IsDevelopment() || builder.Environment.IsEnvironment("Staging"))
                    {
                        corsBuilder
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    }
                    else
                    {
                        corsBuilder
                        .WithOrigins("https://*.grupofarsiman.com", "https://*.grupofarsiman.io")
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    }
                });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var isTesting = builder.Environment.IsEnvironment("test");


        if (!isTesting)
        {

            builder.Services.AddDbContext<TranslogixDBContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionStringFromENV("LOGISTIC_GFS")));


            builder.Services.AddScoped<IUnitOfWork>(serviceProvider =>
            {
                var dbContext = serviceProvider.GetRequiredService<TranslogixDBContext>();
                return new UnitOfWork(dbContext);
            });

        }



        ServiceConfiguration.ConfiguracionServicios(builder.Services);


        builder.Services.AddScoped<UnitOfWorkBuilder>();
        builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowSpecificOrigin");

        app.UseAuthorization();

        app.UseAuthentication();


        app.MapControllers();

        app.Run();
    }
}