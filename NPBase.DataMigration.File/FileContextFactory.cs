using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NPBase.Domain.DataAccess;

namespace NaturalCommerce.Migrations.Admin;

internal class FileContextFactory : IDesignTimeDbContextFactory<FileDataContext>
{
    public FileDataContext CreateDbContext(string[] args)
    {
        var connectionString = @"User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=myDataBase;Pooling=true;";




        var builder = new DbContextOptionsBuilder<FileDataContext>();
        builder.UseNpgsql(connectionString,
            b => b.MigrationsAssembly(GetType().Assembly.GetName().Name)
                .MigrationsHistoryTable("__EFMigrationsHistory", "ef_mig")
        );

        return new FileDataContext(builder.Options);
    }
}