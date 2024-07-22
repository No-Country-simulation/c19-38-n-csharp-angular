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
    public class HistorialRendimientoController : ControllerBase
    {
       private readonly IRepository<HistorialRendimiento> _repository;

        public HistorialRendimientoController(IRepository<HistorialRendimiento> repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HistorialRendimiento>>> GetHistorialRendimiento()
        {
            var historial = await _repository.GetAllAsync();
            if (historial == null)
            {
                return NotFound();
            }
            return Ok(historial);
        }

        [HttpGet("{id}", Name = "getHistorialRendimiento")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<HistorialRendimiento>>> GetHistorialRendimiento(int id)
        {
            var historial = await _repository.GetByIdAsync(id);
            if (historial == null)
            {
                return NotFound();
            }
            return Ok(historial);
        }

        [Authorize]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
        [ProducesResponseType(203)]
        [ProducesResponseType(400, Type = typeof(ProblemDetails))]
        [HttpPost("RegistroHistorialRendimiento")]
        public async Task<IActionResult> RegistroHistorialRendimiento([FromBody] HistorialRendimientoDto historialRendimientoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var historial = Mapper.MapHistorialRendimientoDtoToHistorialRendimiento(historialRendimientoDto);
                await _repository.AddAsync(historial);
                await _repository.SaveChangesAsync();
                return CreatedAtAction(nameof(GetHistorialRendimiento), new { id = historial.IdHistorial }, historial);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving the historialRendimiento to the database");
            }
        }

        [Authorize]
        [HttpPut("{id}", Name = "PutHistorialRendimiento")]
        [ProducesResponseType(201, Type = typeof(HistorialRendimientoDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutHistorialRendimiento(int id, [FromBody] HistorialRendimientoDto historialRendimientoDto)
        {
            var historial = Mapper.MapHistorialRendimientoDtoToHistorialRendimiento(historialRendimientoDto);
            await _repository.EditAsync(historial, id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteHistorialRendimiento")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteHistorialRendimiento(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
