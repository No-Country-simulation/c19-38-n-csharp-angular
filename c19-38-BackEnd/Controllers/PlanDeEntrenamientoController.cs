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
        public async Task<ActionResult<IEnumerable<PlanDeEntrenamiento>>> GetPlanDeEntrenamiento()
        {
            var plan = await _repository.GetAllAsync();
            if (plan == null)
            {
                return NotFound();
            }
            return Ok(plan);
        }

        [HttpGet("{id}", Name = "getPlanDeEntrenamiento")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PlanDeEntrenamiento>>> GetPlanDeEntrenamiento(int id)
        {
            var plan = await _repository.GetByIdAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            return Ok(plan);
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
                return CreatedAtAction(nameof(GetPlanDeEntrenamiento), new { id = plan.IdPlan }, plan);

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
