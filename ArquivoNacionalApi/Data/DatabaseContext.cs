using Microsoft.EntityFrameworkCore;
using ArquivoNacionalApi.Domain.Entities;

namespace ArquivoNacionalApi.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentMetadata> DocumentMetadata { get; set; }
    public DbSet<IndexPoint> IndexPoints { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<User> Users { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        if (!Documents.Any())
        {
            CreateMockData();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }

    private void CreateMockData()
    {
        // Criar alguns documentos
        var documents = new List<Document>
        {
            new Document { Id = Guid.NewGuid(), FilePath = "1nT_q4qU7hpbWHy4vmwiaxryXIZVaTSOz" },
            new Document { Id = Guid.NewGuid(), FilePath = "1eIh6NZFZvyMTADMgVV5dDyrVqDz-hxfl" },
            new Document { Id = Guid.NewGuid(), FilePath = "14XFC6WjazXHJYe6fWtZfZc1QogaqHoOX" }
        };

        // Criar alguns usuários
        var users = new List<User>
        {
            new User { Id = Guid.NewGuid(), Name = "John Doe", Email = "john.doe@example.com", Password = "password123", State = "California" },
            new User { Id = Guid.NewGuid(), Name = "Jane Smith", Email = "jane.smith@example.com", Password = "pass456", State = "New York" },
            new User { Id = Guid.NewGuid(), Name = "Michael Johnson", Email = "michael.j@example.com", Password = "pass789", State = "Texas" }
        };

        // Criar algumas tags
        var tags = new List<IndexPoint>
        {
            new IndexPoint { Id = Guid.NewGuid(), Name = "Important" },
            new IndexPoint { Id = Guid.NewGuid(), Name = "Relevant" },
            new IndexPoint { Id = Guid.NewGuid(), Name = "Insightful" },
            new IndexPoint { Id = Guid.NewGuid(), Name = "Summary" }
        };

        // Associar alguns documentos com tags
        var listMetadata = new List<DocumentMetadata>()
        {

            {
                new DocumentMetadata { Id = Guid.NewGuid(), DocumentId = documents[0].Id, UserId = users[0].Id, Title = "Document 1: Introduction", Context = "This is the introduction of document 1.", SocialMarkers = "Important, Relevant", Points = 15, IndexPoints = new List<IndexPoint> { tags[0], tags[1] } }
            },
            {
                new DocumentMetadata { Id = Guid.NewGuid(), DocumentId = documents[1].Id, UserId = users[1].Id, Title = "Document 2: Chapter 1", Context = "Chapter 1 of document 2 discusses important topics.", SocialMarkers = "Insightful", Points = 20, IndexPoints = new List<IndexPoint> { tags[2] } }
            },
            {
                new DocumentMetadata { Id = Guid.NewGuid(), DocumentId = documents[2].Id, UserId = users[2].Id, Title = "Document 3: Conclusion", Context = "The conclusion of document 3 summarizes key findings.", SocialMarkers = "Summary", Points = 10, IndexPoints = new List<IndexPoint> { tags[3] } }
            }
        };

        // Adicionar documentos, usuários e tags ao contexto
        Documents.AddRange(documents);
        Users.AddRange(users);
        IndexPoints.AddRange(tags);
        DocumentMetadata.AddRange(listMetadata);

        // Salvar as alterações no banco de dados
        SaveChanges();
    }
}

