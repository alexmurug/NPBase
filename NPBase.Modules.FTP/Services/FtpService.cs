using AutoMapper;
using FluentFTP;
using Microsoft.Extensions.Options;
using NPBase.Modules.FTP.Helpers;
using NPBase.Modules.FTP.Interfaces;
using NPBase.Modules.FTP.Models;

namespace NPBase.Modules.FTP.Services;

public class FtpService : IFtpService
{

    private readonly FtpClient _ftpClient;
    private readonly IMapper _mapper;
    private readonly FtpConfigs _config;

    public FtpService(IOptions<FtpConfigs> ftpConfigs, IMapper mapper)
    {
        _config = ftpConfigs.Value;
        _ftpClient = new FtpClient(ftpConfigs.Value.Host, ftpConfigs.Value.UserName, ftpConfigs.Value.Password);
        _mapper = mapper;
    }

    public async Task<int> DownloadFilesAsync(ICollection<string> remotePaths, Progress<FtpProgress> progress, CancellationToken token = default)
    {
        return await _ftpClient.DownloadFilesAsync(@"D:\Downloads", remotePaths, FtpLocalExists.Overwrite, FtpVerify.None, token: token, progress: progress);
    }

    public async Task<ICollection<FileInfoModel>> GetFilesAsync(CancellationToken token = default)
    {
        await _ftpClient.ConnectAsync(token);

        ICollection<FileInfoModel> fileInfos = new List<FileInfoModel>();
        // get a recursive listing of the files & folders in a specific folder
        foreach (var item in await _ftpClient.GetListingAsync(_config.WorkingDirectory ?? _ftpClient.GetWorkingDirectory(), FtpListOption.Recursive, token))
        {
            switch (item.Type)
            {
                case FtpFileSystemObjectType.Directory:
                    break;
                case FtpFileSystemObjectType.File:
                    var fileInfo = _mapper.Map<FileInfoModel>(item);
                    fileInfos.Add(fileInfo);
                    break;
                case FtpFileSystemObjectType.Link:
                    break;
            }
        }

        return fileInfos;
    }
}
