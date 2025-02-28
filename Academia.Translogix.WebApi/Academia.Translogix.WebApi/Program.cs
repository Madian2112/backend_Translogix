using Academia.Translogix.WebApi._Features.Acce.Services;
using Academia.Translogix.WebApi._Features.Gral.Services;
using Academia.Translogix.WebApi.Common;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Farsiman.Extensions.Configuration;

// using Farsiman.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

builder.Services.AddDbContext<TranslogixDBContext>(o => o.UseSqlServer(
                builder.Configuration.GetConnectionStringFromENV("LOGISTIC_GFS")),
                ServiceLifetime.Scoped);

builder.Services.AddScoped<UnitOfWorkBuilder>();

// Servicios de Aplicaci�n
ServiceConfiguration.ConfiguracionServicios(builder.Services);


// Instancia del mapeado
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.UseAuthentication();

//app.UseFsAuthService();

app.MapControllers();

app.Run();
