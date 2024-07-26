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
    public class PlanDeEntrenamientoController : ControllerBase
    {
       private readonly IRepository<PlanDeEntrenamiento> _repository;

        public PlanDeEntrenamientoController(IRepository<PlanDeEntrenamiento> repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PlanDeEntrenamientoDto>>> GetPlanDeEntrenamiento()
        {
            var plan = await _repository.GetAllAsync();
            if (plan == null)
            {
                return NotFound();
            }
            var planDto = plan.Select(p =>Mapper.MapPlanDeEntretamientoToPlanDeEntrenamientoDto(p)).ToList();
            return Ok(planDto);
        }

        [HttpGet("{id}", Name = "getPlanDeEntrenamiento")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PlanDeEntrenamientoDto>>> GetPlanDeEntrenamiento(int id)
        {
            var plan = await _repository.GetByIdAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            var planDto = Mapper.MapPlanDeEntretamientoToPlanDeEntrenamientoDto(plan);
            return Ok(planDto);
        }

        [HttpGet("PlanesDeEntrenamientoSegun/{usuarioId}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(IEnumerable<PlanDeEntrenamientoDto>))]
        public async Task<ActionResult<IEnumerable<PlanDeEntrenamientoDto>>> GetPlanDeEntrenamientoUsuarioId(int usuarioId)
        {
            var planDeEntrnamientoUsuarioId = (await _repository.GetAllAsync()).Where(x => x.IdAutorUsuario == usuarioId).ToList();
            
            if(planDeEntrnamientoUsuarioId is null)
            {
                return Ok(new PlanDeEntrenamientoDto[] { });
            }
            var planesDeEntrenamientoDtos = Mapper.MapListPlanDeEntrenamientoToListPlanDeEntrenamientoDto(planDeEntrnamientoUsuarioId);

            return Ok(planesDeEntrenamientoDtos);
        }
        [HttpGet("PlanesFiltrado")]
        public async Task<ActionResult<IEnumerable<PlanDeEntrenamientoDto>>> GetPlanesFiltradoPaginado(
            [FromQuery] int pagina = 1, 
            [FromQuery] string nombre = "", 
            [FromQuery] bool ordenadoAlfabetico = false,
            [FromQuery] bool ordenadoFecha = false)
        {
            var planes = (await _repository.GetAllAsync()).MapListPlanDeEntrenamientoToListPlanDeEntrenamientoDto().AsQueryable();
            if(!string.IsNullOrEmpty(nombre))
            {
                planes = planes.Where(p => p.Descripcion.Contains(nombre,StringComparison.OrdinalIgnoreCase));  
            }
            if(ordenadoAlfabetico)
            {
                planes = planes.OrderBy(p => p.Descripcion);
            }
            if (ordenadoFecha)
                planes = planes.OrderByDescending(p => p.FechaPublicacion);
            return planes.Skip(pagina - 1).Take(10).ToList();
        }

        [Authorize]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
        [ProducesResponseType(203)]
        [ProducesResponseType(400, Type = typeof(ProblemDetails))]
        [HttpPost("RegistroPlanDeEntrenamiento")]
        public async Task<IActionResult> RegistroPlanDeEntrenamiento([FromBody] PlanDeEntrenamientoDto planDeEntrenamientoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var plan = Mapper.MapPlanDeEntrenamientoDtoToPlanDeEntrenamiento(planDeEntrenamientoDto);
                await _repository.AddAsync(plan);
                await _repository.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPlanDeEntrenamiento), new { id = plan.IdPlan }, planDeEntrenamientoDto);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving the PlanDeEntrenamiento to the database");
            }
        }

        [Authorize]
        [HttpPut("{id}", Name = "PutPlanDeEntrenamiento")]
        [ProducesResponseType(201, Type = typeof(PlanDeEntrenamientoDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutPlanDeEntrenamiento(int id, [FromBody] PlanDeEntrenamientoDto planDeEntrenamientoDto)
        {
            var plan = Mapper.MapPlanDeEntrenamientoDtoToPlanDeEntrenamiento(planDeEntrenamientoDto);
            await _repository.EditAsync(plan, id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeletePlanDeEntrenamiento")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePlanDeEntrenamiento(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
