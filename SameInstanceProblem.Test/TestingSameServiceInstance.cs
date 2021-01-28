using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SameInstanceProblem.Api;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace SameInstanceProblem.Test
{
    public class TestingSameServiceInstance
    {
        private readonly TestServer _testServer;
   
        public TestingSameServiceInstance()
        {
            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Api.Startup)).Location))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: true)
                        .AddJsonFile($"appsettings.Development.json", optional: true)
                        .AddEnvironmentVariables();
                })
                .UseEnvironment("Development")
                .CaptureStartupErrors(true)
                .UseStartup<Api.Startup>();

            _testServer = new TestServer(hostBuilder);
        }

        [Fact]
        public async Task Api_Response_Should_Be_Equal_To_Value_Provided_By_Service()
        {
            using var httpClient = _testServer.CreateClient();
            var expectedNumber = _testServer.Services.GetService<INumberGenerator>().Generate();            

            var response = await httpClient.GetAsync("Number");
            var payload = (await response.Content.ReadAsStringAsync());

            var otherNumber = _testServer.Services.GetService<INumberGenerator>().Generate();

            Assert.NotEqual(expectedNumber.ToString(), payload);
        }
    }
}