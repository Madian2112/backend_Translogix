using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace Translogix.IntegrationTests
{
    public class TestHostEnvironment : IHostEnvironment
    {
        public string EnvironmentName { get; set; } = "test";
        public string ApplicationName { get; set; } = "TestApp";
        public string ContentRootPath { get; set; } = Directory.GetCurrentDirectory();
        public IFileProvider ContentRootFileProvider { get; set; } = new PhysicalFileProvider(Directory.GetCurrentDirectory());
    }
}
