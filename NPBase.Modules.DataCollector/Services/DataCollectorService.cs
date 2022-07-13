using FluentFTP;
using NPBase.Domain.DataAccess.Interfaces;
using NPBase.Domain.DataAccess.Models.Entities;
using NPBase.Modules.DataCollector.Interfaces;
using NPBase.Modules.FTP.Interfaces;
using NPBase.Modules.FTP.Models;

namespace NPBase.Modules.DataCollector.Services;

public class DataCollectorService : IDataCollector
{
    private readonly IFtpService _ftpService;
    private readonly IFileRepository _fileRepository;
    private readonly Progress<FtpProgress> _progress;
    private List<string> _downloadeFilePath;

    public DataCollectorService(IFtpService ftpService, IFileRepository fileRepository)
    {
        _ftpService = ftpService;
        _fileRepository = fileRepository;
        _progress = new Progress<FtpProgress>();
    }

    public Progress<FtpProgress> Callback { get; private set; }

    public async Task RunAsync(CancellationToken stoppingToken)
    {
        var ftpFiles = await _ftpService.GetFilesAsync(stoppingToken);

        var newFiles = await GetNewFilePathsAsync(ftpFiles, stoppingToken);

        _progress.ProgressChanged += ProgressDownloadingFile;
       
        _downloadeFilePath = new List<string>();
       
        await _ftpService.DownloadFilesAsync(newFiles, _progress, stoppingToken);

        await AddFileToDBAsync(ftpFiles, newFiles);
    }

    private async Task AddFileToDBAsync(ICollection<FileInfoModel> ftpFiles, List<string> newFiles)
    {
        var filesToAdd = ftpFiles.Where(x => newFiles.Contains(x.FullPath)).Select(x => new FileEntity
        {
            FullPath = x.FullPath,
            CreatedAt = x.CreatedDate,
            ModifiedAt = x.ModifiedDate,
            Name = x.Name
        }).ToList();

        await _fileRepository.AddRangeAsync(filesToAdd);
    }

    private void ProgressDownloadingFile(object? sender, FtpProgress e)
    {
        if (e.Progress == 100)
        {
            _downloadeFilePath.Add(e.RemotePath);
        }
        else
        {
            // percent done = (p.Progress * 100)
        }
    }

    private async Task<List<string>> GetNewFilePathsAsync(ICollection<FileInfoModel> ftpFiles, CancellationToken token)
    {
        var savedFiles = await _fileRepository.GetByFullPath(ftpFiles.Select(x => x.FullPath).ToList());

        var savedPath = savedFiles.Select(x => x.FullPath);
        var newFilePaths = ftpFiles.Select(x => x.FullPath).Except(savedPath).ToList();

        return newFilePaths;
    }
}
