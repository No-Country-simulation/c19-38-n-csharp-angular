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
    public class SerieController : ControllerBase
    {
        private readonly IRepository<Serie> _repository;

        public SerieController(IRepository<Serie> repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SerieDto>>>GetSerie()
        {
            var series = await _repository.GetAllAsync();
            if (series == null)
            {
                return NotFound();
            }
            var seriesDto = series.Select(e=>Mapper.MapSerieToSerieDto(e)).ToList();
            return Ok(seriesDto);
        }

        [HttpGet("{id}", Name = "getSerie")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SerieDto>>>GetSerie(int id)
        {
            var serie = await _repository.GetByIdAsync(id);
            if (serie == null)
            {
                return NotFound();
            }
            var serieDto = Mapper.MapSerieToSerieDto(serie);
            return Ok(serieDto);
        }

        [Authorize]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
        [ProducesResponseType(203)]
        [ProducesResponseType(400, Type = typeof(ProblemDetails))]
        [HttpPost("Registro")]
        public async Task<IActionResult> RegistroSerie([FromBody] SerieDto serieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var serie = Mapper.MapSerieDtoToSerie(serieDto);
                await _repository.AddAsync(serie);
                await _repository.SaveChangesAsync();
                return CreatedAtAction(nameof(GetSerie), new {id = serie.IdSerie}, serieDto);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving the series to the database");
            }
        }

        [Authorize]
        [HttpPut("{id}", Name = "PutSerie")]
        [ProducesResponseType(201, Type = typeof(SerieDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutSerie(int id, [FromBody] SerieDto serieDto)
        {
            var serie = Mapper.MapSerieDtoToSerie(serieDto);
            await _repository.EditAsync(serie, id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteSerie")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSerie(int id)
        {
           await _repository.DeleteAsync(id);
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
