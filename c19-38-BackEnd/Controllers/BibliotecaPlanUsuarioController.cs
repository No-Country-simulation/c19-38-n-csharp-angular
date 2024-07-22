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
    public class BibliotecaPlanUsuarioController : Controller
    {
        private readonly IRepository<BibliotecaPlanUsuario> _repository;

        public BibliotecaPlanUsuarioController(IRepository<BibliotecaPlanUsuario> repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BibliotecaPlanUsuario>>> GetBibliotecaPlanUsuario()
        {
            var biblioteca = await _repository.GetAllAsync();
            if (biblioteca == null)
            {
                return NotFound();
            }
            return Ok(biblioteca);
        }

        [HttpGet("{id}", Name = "getBibliotecaPlanUsuario")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<BibliotecaPlanUsuario>>> GetBibliotecaPlanUsuario(int id)
        {
            var biblioteca = await _repository.GetByIdAsync(id);
            if (biblioteca == null)
            {
                return NotFound();
            }
            return Ok(biblioteca);
        }

        [Authorize]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
        [ProducesResponseType(203)]
        [ProducesResponseType(400, Type = typeof(ProblemDetails))]
        [HttpPost("RegistroBibliotecaPlanUsuario")]
        public async Task<IActionResult> RegistroBibliotecaPlanUsuario([FromBody] BibliotecaPlanUsuarioDto bibliotecaPlanUsuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var biblioteca = Mapper.MapBibliotecaPlanUsuarioDtoToBibliotecaPlanUsuario(bibliotecaPlanUsuarioDto);
                await _repository.AddAsync(biblioteca);
                await _repository.SaveChangesAsync();
                return CreatedAtAction(nameof(GetBibliotecaPlanUsuario), new { id = biblioteca.IdBiblioteca }, biblioteca);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving the BibliotecaPlanUsuario to the database");
            }
        }

        [Authorize]
        [HttpPut("{id}", Name = "PutBibliotecaPlanUsuario")]
        [ProducesResponseType(201, Type = typeof(BibliotecaPlanUsuarioDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutBibliotecaPlanUsuario(int id, [FromBody] BibliotecaPlanUsuarioDto bibliotecaPlanUsuarioDto)
        {
            var biblioteca = Mapper.MapBibliotecaPlanUsuarioDtoToBibliotecaPlanUsuario(bibliotecaPlanUsuarioDto);
            await _repository.EditAsync(biblioteca, id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteBibliotecaPlanUsuario")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBibliotecaPlanUsuario(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
