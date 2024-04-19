using ArquivoNacionalApi.Data.Repositories;
using ArquivoNacionalApi.Domain.Dtos;
using ArquivoNacionalApi.Domain.Entities;

namespace ArquivoNacionalApi.Services
{
    public class DocumentMetadataService
    {
        private readonly IDocumentMetadataRepository _documentMetadataRepository;
        private readonly IIndexRepository _indexRepository;

        public DocumentMetadataService(IDocumentMetadataRepository documentMetadataRepository, IIndexRepository indexRepository)
        {
            _documentMetadataRepository = documentMetadataRepository;
            _indexRepository = indexRepository;
        }

        public async Task<IEnumerable<DocumentMetadata>> GetAllDocumentMetadataAsync()
        {
            return await _documentMetadataRepository.GetAllAsync();
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

                UpdateIndexPoints(documentMetadataFound, documentMetadataDto.IndexPoints);
                UpdateMetadataPoints(documentMetadataFound, documentMetadataDto);
                
                _documentMetadataRepository.Update(documentMetadataFound);
                return true;
            }
            return false;
        }

        private void UpdateMetadataPoints(DocumentMetadata documentMetadata, UpdateDocumentMetadataDTO documentMetadataDto)
        { }

        private void UpdateIndexPoints(DocumentMetadata documentMetadata, List<IndexPointDTO> listIndexDto)
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
                        Id = Guid.NewGuid(),
                        Name = i.Name ?? ""
                    });

                documentMetadata.IndexPoints.AddRange(newIndexPoints);
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
}