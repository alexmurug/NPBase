using NPBase.Modules.DataCollector.Interfaces;

namespace NPBase.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDataCollector _dataCollector;

        public Worker(ILogger<Worker> logger, IDataCollector dataCollector)
        {
            _logger = logger;
            _dataCollector = dataCollector;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await _dataCollector.RunAsync(stoppingToken);

                await Task.Delay(60 * 100, stoppingToken);
            }
        }
    }
}