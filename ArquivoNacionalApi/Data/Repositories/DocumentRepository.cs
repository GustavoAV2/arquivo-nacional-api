using ArquivoNacionalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArquivoNacionalApi.Data.Repositories
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(DatabaseContext context) : base(context)
        { }
    }

    public interface IDocumentRepository : IRepository<Document>
    {
       
    }
}