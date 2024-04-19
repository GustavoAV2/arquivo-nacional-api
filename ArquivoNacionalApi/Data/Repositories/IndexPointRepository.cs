﻿using ArquivoNacionalApi.Domain.Entities;

namespace ArquivoNacionalApi.Data.Repositories
{
    public class IndexPointRepository : Repository<IndexPoint>, IIndexRepository
    {
        public IndexPointRepository (DatabaseContext context) : base(context)
        { }

        public List<IndexPoint> GetListIndexByName(List<string> names)
        { 
            return _context.IndexPoints
                .Where(i => names.Any(nameI => nameI == i.Name.ToLower()))
                .ToList();
        }
    }

    public interface IIndexRepository : IRepository<IndexPoint> 
    {
        List<IndexPoint> GetListIndexByName(List<string> names);
    }
}