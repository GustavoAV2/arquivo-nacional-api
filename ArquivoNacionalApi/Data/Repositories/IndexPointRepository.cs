using ArquivoNacionalApi.Domain.Entities;

namespace ArquivoNacionalApi.Data.Repositories
{
    public class IndexPointRepository : Repository<IndexPoint>, IIndexPointRepository
    {
        public IndexPointRepository (DatabaseContext context) : base(context)
        { }

        public List<IndexPoint> GetListIndexByName(List<string> names)
        { 
            return _context.IndexPoints
                .Where(i => names.Any(nameI => nameI == i.Name.ToLower()))
                .ToList();
        }
        public List<IndexPoint> GetIndexMatchs(Guid documentId)
        {
            var indexsSemelhantes = _context.IndexPoints
                    .Where(tag1 => tag1.DocumentsMetadata.Any(d => d.DocumentId == documentId) 
                                   && _context.IndexPoints.Any(tag2 => tag1.Name.ToLower().Contains(tag2.Name.ToLower())))
                    .ToList();
            return indexsSemelhantes;
        }

        public async Task AddListIndexAsync(List<IndexPoint> indexPoints)
        {
            await _context.AddRangeAsync(indexPoints);
            await SaveChangeAsync();
        }
    }

    public interface IIndexPointRepository : IRepository<IndexPoint> 
    {
        List<IndexPoint> GetListIndexByName(List<string> names);

        List<IndexPoint> GetIndexMatchs(Guid documentId);

        Task AddListIndexAsync(List<IndexPoint> indexPoint);
    }
}
