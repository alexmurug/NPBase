namespace NPBase.Domain.DataAccess.Models.Entities;

public class FileEntity : BaseEntity
{
    public string Name { get; set; }

    public string FullPath { get; set; }

    public long Size { get; set; }
}
