using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OptionsBindTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateApplicationBuilder(args);

            host.Configuration.AddJsonFile("appsettings.json", false, true);

            host.Services.AddOptions();
            host.Services.AddLogging();

            /*****This will NOT update the options if the value in the JSON file is changed:*****/

            //host.Services.AddOptions<TestOptions>().Configure<IConfiguration>((options, config) =>
            //{
            //    config.Bind(options);
            //});

            /*****This will update the options if the value in the JSON file changes*****/
            host.Services.AddOptions<TestOptions>().BindConfiguration(string.Empty);

            host.Services.AddHostedService<TestService>();

            var app = host.Build();

            await app.RunAsync();
        }
    }
}
