using Microsoft.EntityFrameworkCore;
using NPBase.Domain.DataAccess.Interfaces;
using NPBase.Domain.DataAccess.Models.Entities;

namespace NPBase.Domain.DataAccess.Repositories;

public class FileRepository : IFileRepository
{
    private readonly FileDataContext _dataContext;

    public FileRepository(FileDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task AddRangeAsync(List<FileEntity> entities, CancellationToken token = default)
    {
        await _dataContext.AddRangeAsync(entities, token);
    }

    public Task<List<FileEntity>> GetByFullPath(List<string> fullPaths, CancellationToken token = default)
    {
        return _dataContext.Set<FileEntity>().Where(x => fullPaths.Contains(x.FullPath)).ToListAsync(cancellationToken: token);
    }
}
