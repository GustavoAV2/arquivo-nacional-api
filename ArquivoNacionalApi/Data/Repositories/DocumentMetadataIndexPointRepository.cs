using ArquivoNacionalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ArquivoNacionalApi.Data.Repositories
{
    public class DocumentMetadataIndexPointRepository : Repository<DocumentMetadataIndexPoint>, IDocumentMetadataIndexPointRepository
    {
        public DocumentMetadataIndexPointRepository(DatabaseContext context) : base(context) { }

    }

    public interface IDocumentMetadataIndexPointRepository : IRepository<DocumentMetadataIndexPoint>
    {
    }
   
}
