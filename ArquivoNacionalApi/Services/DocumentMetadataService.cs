using ArquivoNacionalApi.Data.Repositories;
using ArquivoNacionalApi.Domain.Dtos;
using ArquivoNacionalApi.Domain.Entities;
using System.Linq;

namespace ArquivoNacionalApi.Services
{
    public class DocumentMetadataService : IDocumentMetadataService
    {
        private readonly IDocumentMetadataRepository _documentMetadataRepository;
        private readonly IIndexPointRepository _indexRepository;

        public DocumentMetadataService(IDocumentMetadataRepository documentMetadataRepository, IIndexPointRepository indexRepository)
        {
            _documentMetadataRepository = documentMetadataRepository;
            _indexRepository = indexRepository;
        }

        public async Task<IEnumerable<DocumentMetadata>> GetAllDocumentMetadataAsync()
        {
            return await _documentMetadataRepository.GetAllDocumentMetadataAsync();
        }

        public async Task<DocumentMetadata> GetDocumentMetadataByIdAsync(Guid id)
        {
            return await _documentMetadataRepository.GetByIdAsync(id);
        }

        public void CreateDocumentMetadata(CreateDocumentMetadataDTO documentMetadataDto)
        {
            var documentMetadata = new DocumentMetadata { 
                Id = Guid.NewGuid(),
                DocumentId = documentMetadataDto.DocumentId,
                UserId = documentMetadataDto.UserId,
                Context = documentMetadataDto.Context,
                SocialMarkers = documentMetadataDto.SocialMarkers,
                Title = documentMetadataDto.Title 
            };

            UpdateIndexPoints(documentMetadata, documentMetadataDto.IndexPoints);
            _documentMetadataRepository.Add(documentMetadata);
        }

        public async Task<bool> UpdateDocumentMetadataAsync(Guid userId, Guid documentId, UpdateDocumentMetadataDTO documentMetadataDto)
        {
            var documentMetadataFound = _documentMetadataRepository.GetDocumentMetadataByUserIdAndDocumentIdAsync(userId, documentId);

            if (documentMetadataFound != null)
            {
                documentMetadataFound.Title = documentMetadataDto.Title ?? documentMetadataFound.Title;
                documentMetadataFound.SocialMarkers = documentMetadataDto.SocialMarkers ?? documentMetadataFound.SocialMarkers;
                documentMetadataFound.Context = documentMetadataDto.Context ?? documentMetadataFound.Context;

                await UpdateIndexPoints(documentMetadataFound, documentMetadataDto.IndexPoints);
                UpdateMetadataPoints(documentMetadataFound, documentMetadataDto);
                
                await _documentMetadataRepository.UpdateMetadata(documentMetadataFound);
                return true;
            }
            return false;
        }

        private void UpdateMetadataPoints(DocumentMetadata documentMetadata, UpdateDocumentMetadataDTO documentMetadataDto)
        {
            var betterMetadatas = _indexRepository.GetIndexMatchs(documentMetadata.DocumentId);
            var points = 5;

            foreach (var indexPoint in documentMetadataDto.IndexPoints)
            {
                if (points == 20)
                {
                    break;
                }

                if (betterMetadatas.Any(b => b.Name.Contains(indexPoint.Name.ToLower())))
                {
                    points++;
                }
            }
            documentMetadata.Points = points;
        }

        private async Task UpdateIndexPoints(DocumentMetadata documentMetadata, List<IndexPointDTO> listIndexDto)
        {
            if (listIndexDto.Count > 0)
            {
                var nameInsertedIndex = listIndexDto.Select(i => i.Name);

                var foundIndex = new List<IndexPoint>();
                if (nameInsertedIndex != null)
                {
                    foundIndex = _indexRepository.GetListIndexByName(nameInsertedIndex.ToList());
                    documentMetadata.IndexPoints.AddRange(foundIndex);
                }

                var newIndexPoints = listIndexDto
                    .Where(i => !foundIndex.Any(foundI => foundI.Name == i.Name))
                    .Select(i => new IndexPoint()
                    {
                        DocumentsMetadata = new List<DocumentMetadata> { documentMetadata },
                        Id = Guid.NewGuid(),
                        Name = i.Name ?? ""
                    });

                await _indexRepository.AddListIndexAsync(newIndexPoints.ToList());
            }
        }

        public async Task<bool> DeleteDocumentMetadataAsync(Guid id)
        {
            var documentMetadataFound = await GetDocumentMetadataByIdAsync(id);
            if (documentMetadataFound != null)
            {
                _documentMetadataRepository.Delete(documentMetadataFound);
                return true;
            }
            return false;
        }
    }

    public interface IDocumentMetadataService
    {
        Task<IEnumerable<DocumentMetadata>> GetAllDocumentMetadataAsync();
        Task<DocumentMetadata> GetDocumentMetadataByIdAsync(Guid id);
        void CreateDocumentMetadata(CreateDocumentMetadataDTO documentMetadataDto);
        Task<bool> UpdateDocumentMetadataAsync(Guid userId, Guid documentId, UpdateDocumentMetadataDTO documentMetadataDto);
        Task<bool> DeleteDocumentMetadataAsync(Guid id);
    }
}