using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RatedMoviesDemo.Repository;

namespace RatedMoviesDemo.Api.Tests.Utilities
{
    public class InMemoryTestServer : IDisposable
    {
        private readonly TestServer _testServer;
        public HttpClient Client { get; }

        public InMemoryTestServer(DbContextOptions<RatedMoviesContext> dbContextOptions)
        {
            var builder = new WebHostBuilder()
                .UseSolutionRelativeContentRoot("RatedMoviesDemo.Api")
                .UseEnvironment("Development")
                .ConfigureTestServices(_ => _.AddSingleton(typeof(RatedMoviesContext), new RatedMoviesContext(dbContextOptions)))
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
