using ArquivoNacionalApi.Domain.Dtos;
using ArquivoNacionalApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArquivoNacionalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndexPointController : ControllerBase
    {
        private readonly IndexPointService _indexPointService;

        public IndexPointController(IndexPointService indexPointService)
        {
            _indexPointService = indexPointService;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<IndexPointDTO>>> GetAllIndexPoints()
        {
            var indexPointDtos = await _indexPointService.GetAllIndexPointsAsync();
            return Ok(indexPointDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IndexPointDTO>> GetIndexPoint(Guid id)
        {
            var indexPointDto = await _indexPointService.GetIndexPointByIdAsync(id);
            if (indexPointDto == null)
                return NotFound();

            return Ok(indexPointDto);
        }

        [HttpPost()]
        public ActionResult CreateIndexPoint([FromBody] IndexPointDTO indexPointDto)
        {
            _indexPointService.CreateIndexPoint(indexPointDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async  Task<IActionResult> UpdateIndexPoint(Guid id, [FromBody] IndexPointDTO indexPointDto)
        {
            await _indexPointService.UpdateIndexPointAsync(id, indexPointDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndexPoint(Guid id)
        {
            await _indexPointService.DeleteIndexPointAsync(id);
            return NoContent();
        }
    }
}
