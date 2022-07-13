using Microsoft.EntityFrameworkCore;
using NPBase.Domain.DataAccess;
using NPBase.Domain.DataAccess.Interfaces;
using NPBase.Domain.DataAccess.Repositories;
using NPBase.Modules.DataCollector.Interfaces;
using NPBase.Modules.DataCollector.Services;
using NPBase.Modules.FTP.Helpers;
using NPBase.Modules.FTP.Interfaces;
using NPBase.Modules.FTP.Profiles;
using NPBase.Modules.FTP.Services;
using NPBase.WorkerService;



IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddAutoMapper(typeof(FileInfoModelMapProfile));
        services.AddSingleton<IFtpService, FtpService>();
        services.AddSingleton<IFileRepository, FileRepository>();
        services.AddSingleton<IDataCollector, DataCollectorService>();
        services.Configure<FtpConfigs>(context.Configuration.GetSection(FtpConfigs.SectionName));
        services.AddDbContext<FileDataContext>(o => o.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=myDataBase;Pooling=true;"), ServiceLifetime.Singleton, ServiceLifetime.Singleton);
    })
    .Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

await host.RunAsync();
