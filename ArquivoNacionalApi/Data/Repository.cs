using Microsoft.EntityFrameworkCore;

namespace ArquivoNacionalApi.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal readonly DatabaseContext _context;
        internal readonly DbSet<T> _dbSet;

        public Repository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _context.Add(entity);
            _ = SaveChangeAsync();
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _ = SaveChangeAsync();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _ = SaveChangeAsync();
        }

        public async Task<bool> SaveChangeAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
