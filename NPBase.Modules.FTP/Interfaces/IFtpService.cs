using FluentFTP;
using NPBase.Modules.FTP.Models;

namespace NPBase.Modules.FTP.Interfaces;

public interface IFtpService
{
    Task<ICollection<FileInfoModel>> GetFilesAsync(CancellationToken token = default);

    Task<int> DownloadFilesAsync(ICollection<string> remotePaths, Progress<FtpProgress> progress = default, CancellationToken token = default);
}
