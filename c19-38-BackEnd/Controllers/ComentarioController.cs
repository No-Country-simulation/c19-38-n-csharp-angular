using c19_38_BackEnd.Dtos;
using c19_38_BackEnd.Interfaces;
using c19_38_BackEnd.Map;
using c19_38_BackEnd.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace c19_38_BackEnd.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ComentarioController : Controller
    {
        private readonly IRepository<Comentario> _repository;

        public ComentarioController(IRepository<Comentario> repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentario()
        {
            var comentario = await _repository.GetAllAsync();
            if (comentario == null)
            {
                return NotFound();
            }
            return Ok(comentario);
        }

        [HttpGet("{id}", Name = "getComentario")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentario(int id)
        {
            var comentario = await _repository.GetByIdAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }
            return Ok(comentario);
        }

        [Authorize]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
        [ProducesResponseType(203)]
        [ProducesResponseType(400, Type = typeof(ProblemDetails))]
        [HttpPost("RegistroComentario")]
        public async Task<IActionResult> RegistroEjercicio([FromBody] ComentarioDto comentarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var comentario = Mapper.MapComentarioDtoToComentario(comentarioDto);
                await _repository.AddAsync(comentario);
                await _repository.SaveChangesAsync();
                return CreatedAtAction(nameof(GetComentario), new { id = comentario.IdComentario }, comentario);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving the comentario to the database");
            }
        }


        [Authorize]
        [HttpPut("{id}", Name = "PutComentario")]
        [ProducesResponseType(201, Type = typeof(ComentarioDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutComentario(int id, [FromBody] ComentarioDto comentarioDto)
        {
            var comentario = Mapper.MapComentarioDtoToComentario(comentarioDto);
            await _repository.EditAsync(comentario, id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteComentario")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
