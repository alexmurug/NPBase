using Microsoft.EntityFrameworkCore;
using NPBase.Domain.DataAccess.Models.Entities;

namespace NPBase.Domain.DataAccess;

public class FileDataContext : DbContext
{

    public FileDataContext(DbContextOptions<FileDataContext> options) : base(options)
    {
    }

    DbSet<FileEntity> Files { get; set; }
}
