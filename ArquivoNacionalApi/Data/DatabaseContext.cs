using Microsoft.EntityFrameworkCore;
using ArquivoNacionalApi.Domain.Entities;

namespace ArquivoNacionalApi.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentMetadata> DocumentMetadata { get; set; }
    public DbSet<DocumentMetadataIndexPoint> DocumentMetadataIndexPoints { get; set; }
    public DbSet<IndexPoint> IndexPoints { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<User> Users { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }
}