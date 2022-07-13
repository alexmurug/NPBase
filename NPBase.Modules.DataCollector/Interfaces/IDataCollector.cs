namespace NPBase.Modules.DataCollector.Interfaces;

public interface IDataCollector
{
    Task RunAsync(CancellationToken stoppingToken);
}
