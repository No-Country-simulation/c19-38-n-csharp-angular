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
    public class EjercicioController : ControllerBase
    {
        private readonly IRepository<Ejercicio> _repository;

        public EjercicioController(IRepository<Ejercicio> repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Ejercicio>>> GetEjercicio()
        {
            var ejercicio = await _repository.GetAllAsync();
            if (ejercicio == null)
            {
                return NotFound();
            }
            return Ok(ejercicio);
        }

        [HttpGet("{id}", Name = "getEjercicio")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Ejercicio>>> GetEjercicio(int id)
        {
            var ejercicio = await _repository.GetByIdAsync(id);
            if (ejercicio == null)
            {
                return NotFound();
            }
            return Ok(ejercicio);
        }

        [Authorize]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
        [ProducesResponseType(203)]
        [ProducesResponseType(400, Type = typeof(ProblemDetails))]
        [HttpPost("RegistroEjercicio")]
        public async Task<IActionResult> RegistroEjercicio([FromBody] EjercicioDto ejercicioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var ejercicio = Mapper.MapEjercicioDtoToEjercicio(ejercicioDto);
                await _repository.AddAsync(ejercicio);
                await _repository.SaveChangesAsync();
                return CreatedAtAction(nameof(GetEjercicio), new { id = ejercicio.IdEjercicio }, ejercicio);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving the series to the database");
            }
        }

        [Authorize]
        [HttpPut("{id}", Name = "PutEjercicio")]
        [ProducesResponseType(201, Type = typeof(EjercicioDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutEjercicio(int id, [FromBody] EjercicioDto ejercicioDto)
        {
            var ejercicio = Mapper.MapEjercicioDtoToEjercicio(ejercicioDto);
            await _repository.EditAsync(ejercicio, id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteEjercicio")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteEjercicio(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
