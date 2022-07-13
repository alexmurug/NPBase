using NPBase.Domain.DataAccess.Models.Entities;

namespace NPBase.Domain.DataAccess.Interfaces;

public interface IFileRepository
{
    Task<List<FileEntity>> GetByFullPath(List<string> fullPaths, CancellationToken token = default);

    Task AddRangeAsync(List<FileEntity> entities, CancellationToken token = default);
}