namespace NPBase.Modules.FTP.Models;

public class FileInfoModel
{
    public string Name { get; set; }

    public string FullPath { get; set; }

    public long Size { get; set; }

    public DateTime ModifiedDate { get; set; }

    public DateTime CreatedDate { get; set; }
}
