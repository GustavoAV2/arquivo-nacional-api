using ArquivoNacionalApi.Domain.Dtos;
using ArquivoNacionalApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArquivoNacionalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [EndpointDescription("Criar uma sessao, com um unico Player vinculado e um documento")]
        [HttpPost()]
        public async Task<ActionResult> CreateSession([FromBody] CreateSessionDTO sessionDto)
        {
            await _sessionService.CreateSession(sessionDto);
            return Ok();
        }

        [EndpointDescription("Buscar a sessao ativa para o Usuario, junto dos dados de pontuação dos players")]
        [HttpGet("/active/{id}/players")]
        public ActionResult<SessionDTO> GetSessionByUserId(Guid id)
        {
            var sessionDto = _sessionService.GetActiveSessionInfoByUserId(id);
            if (sessionDto == null)
                return NotFound();

            return Ok(sessionDto);
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<SessionDTO>> GetSessionsForUserById([FromQuery] Guid userId)
        {
            var sessionDtos = _sessionService.GetSessionsListByUserIdAsync(userId);
            return Ok(sessionDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SessionDTO>> GetSession(Guid id)
        {
            var sessionDto = await _sessionService.GetSessionByIdAsync(id);
            if (sessionDto == null)
                return NotFound();

            return Ok(sessionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSession(Guid id, [FromBody] UpdateSessionDTO sessionDto)
        {
            if (await _sessionService.UpdateSessionAsync(id, sessionDto))
            {
                return Ok();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(Guid id)
        {
            if (await _sessionService.DeleteSessionAsync(id))
            {
                return Ok();
            }
            return NoContent();
        }
    }
}
