FROM harbor-01.docker-registry.grupofarsiman.io/external/aspnet:8.0

WORKDIR /app

COPY ./compilado .

EXPOSE 8080

ENTRYPOINT dotnet Academia.Translogix.WebApi.dll