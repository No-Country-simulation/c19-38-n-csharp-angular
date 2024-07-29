using c19_38_BackEnd.Dtos;
using c19_38_BackEnd.Interfaces;
using c19_38_BackEnd.Map;
using c19_38_BackEnd.Modelos;
using c19_38_BackEnd.Servicios;
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
        public async Task<ActionResult<IEnumerable<ComentarioDto>>> GetComentario()
        {
            var comentario = await _repository.GetAllAsync();
            if (comentario == null)
            {
                return NotFound();
            }
            var comentarioDto = comentario.Select(x =>Mapper.MapComentarioToComentarioDto(x)).ToList();
            return Ok(comentarioDto);
        }

        //[HttpGet("{id}", Name = "getComentario")]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<IEnumerable<ComentarioDto>>> GetComentario(int id)
        //{
        //    var comentario = await _repository.GetByIdAsync(id);
        //    if (comentario == null)
        //    {
        //        return NotFound();
        //    }
        //    var comentarioDto = Mapper.MapComentarioToComentarioDto(comentario);
        //    return Ok(comentarioDto);
        //}

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("CretePost")]
        public async Task<IActionResult> CreateCooment([FromBody] CreateComentarioDto createCommentDto)
        {
            var userIdClaim = User.Claims.First(c => c.Type == "id");

            var commentToCreate = createCommentDto.MapCreateComentarioDtoToComentario();
            commentToCreate.IdAutor = int.Parse(userIdClaim.Value);
            try
            {
                await _repository.AddAsync(commentToCreate);
                await _repository.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500);
            }

            return Ok();
        }


        //[Authorize]
        //[HttpPut("{id}", Name = "PutComentario")]
        //[ProducesResponseType(201, Type = typeof(ComentarioDto))]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> PutComentario(int id, [FromBody] ComentarioDto comentarioDto)
        //{
        //    var comentario = Mapper.MapComentarioDtoToComentario(comentarioDto);
        //    await _repository.EditAsync(comentario, id);
        //    await _repository.SaveChangesAsync();
        //    return NoContent();
        //}

        [Authorize]
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
