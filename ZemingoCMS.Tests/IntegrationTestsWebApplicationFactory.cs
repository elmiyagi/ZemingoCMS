using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ZemingoCMS.Tests
{
    public class IntegrationTestsWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
    {
        public string HostUrl { get; set; } = "https://localhost:22137";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseUrls(HostUrl);
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            var dummyHost = builder.Build();

            builder.ConfigureWebHost(webHostBuilder => webHostBuilder.UseKestrel());

            var host = builder.Build();
            host.Start();

            var server = host.Services.GetRequiredService<IServer>();
            var addresses = server.Features.Get<IServerAddressesFeature>();

            ClientOptions.BaseAddress = addresses!.Addresses
                .Select(x => new Uri(x))
                .Last();

            dummyHost.Start();
            return dummyHost;
        }
    }
}
