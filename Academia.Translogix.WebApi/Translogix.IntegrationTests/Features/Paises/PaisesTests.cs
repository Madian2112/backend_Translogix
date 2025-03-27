using Microsoft.VisualStudio.TestPlatform.TestHost;

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



    }
}