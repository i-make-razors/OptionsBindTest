using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace OptionsBindTest
{
    public class TestService : BackgroundService
    {
        private readonly IOptionsMonitor<TestOptions> _optionsMonitor;
        private readonly ILogger<TestService> _logger;

        public TestService(IOptionsMonitor<TestOptions> optionsMonitor, ILogger<TestService> logger)
        {
            _optionsMonitor = optionsMonitor;
            _logger = logger;
            _optionsMonitor.OnChange(options => 
            {
                _logger.LogInformation("Options Monitor On Change Raised!");
            });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Value in options: {value}", _optionsMonitor.CurrentValue.TestProperty);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
