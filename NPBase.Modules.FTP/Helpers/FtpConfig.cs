namespace NPBase.Modules.FTP.Helpers;

public class FtpConfigs
{
    public const string SectionName = "FtpConfigs";

    public string Host { get; set; }

    public string Port { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string WorkingDirectory { get; set; }
}