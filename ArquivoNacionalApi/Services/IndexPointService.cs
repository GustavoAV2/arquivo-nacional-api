using ArquivoNacionalApi.Data.Repositories;
using ArquivoNacionalApi.Domain.Dtos;
using ArquivoNacionalApi.Domain.Entities;

namespace ArquivoNacionalApi.Services
{
    public class IndexPointService : IIndexPointService
    {
        private readonly IIndexPointRepository _indexPointRepository;

        public IndexPointService(IIndexPointRepository indexPointRepository)
        {
            _indexPointRepository = indexPointRepository;
        }

        public async Task<IEnumerable<IndexPointDTO>> GetAllIndexPointsAsync()
        {
            var indexPoints = await _indexPointRepository.GetAllAsync();
            return indexPoints.Select(ip => new IndexPointDTO { Id = ip.Id, Name = ip.Name });
        }

        public async Task<IndexPointDTO> GetIndexPointByIdAsync(Guid id)
        {
            var indexPoint = await _indexPointRepository.GetByIdAsync(id);
            return new IndexPointDTO { Id = indexPoint.Id, Name = indexPoint.Name };
        }

        public void CreateIndexPoint(IndexPointDTO indexPoint)
        {
            var newIndexPoint = new IndexPoint() { Id = Guid.NewGuid(), Name = indexPoint.Name };
            _indexPointRepository.Add(newIndexPoint);
        }

        public async Task<bool> UpdateIndexPointAsync(Guid id, IndexPointDTO indexPointDto)
        {
            var indexPoint = await _indexPointRepository.GetByIdAsync(id);
            if (indexPoint != null)
            {
                indexPoint.Name = indexPointDto.Name ?? string.Empty;
                _indexPointRepository.Update(indexPoint);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteIndexPointAsync(Guid id)
        {
            var indexPoint = await _indexPointRepository.GetByIdAsync(id);
            if (indexPoint != null)
            {
                _indexPointRepository.Delete(indexPoint);
                return true;
            }
            return false;
        }
    }

    public interface IIndexPointService
    {
        Task<IEnumerable<IndexPointDTO>> GetAllIndexPointsAsync();
        Task<IndexPointDTO> GetIndexPointByIdAsync(Guid id);
        void CreateIndexPoint(IndexPointDTO indexPoint);
        Task<bool> UpdateIndexPointAsync(Guid id, IndexPointDTO indexPointDto);
        Task<bool> DeleteIndexPointAsync(Guid id);
    }
}
