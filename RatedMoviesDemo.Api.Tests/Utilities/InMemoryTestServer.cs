using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace RatedMoviesDemo.Api.Tests.Utilities
{
    public class InMemoryTestServer : IDisposable
    {
        private readonly TestServer _testServer;
        public HttpClient Client { get; }

        public InMemoryTestServer()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(@"C:\Users\Ravindra\source\repos\RatedMoviesDemo\RatedMoviesDemo.Api")
                .UseEnvironment("Development")
                .UseStartup<Startup>();

            _testServer = new TestServer(builder);
            Client = _testServer.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }
    }
}
