using c19_38_BackEnd.Configuracion;
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
    public class PlanDeEntrenamientoController : ControllerBase
    {
        private readonly IRepository<PlanDeEntrenamiento> _repository;
        private readonly ICloudMediaService _cloudMediaService;

        public PlanDeEntrenamientoController(IRepository<PlanDeEntrenamiento> repository, ICloudMediaService cloudMediaService)
        {
            _repository = repository;
            _cloudMediaService = cloudMediaService;
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

        [Authorize(Policy = Roles.Entrenador)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("CreatePlanDeEntrenamiento")]
        public async Task<IActionResult> CreatePlanDeEntrenamiento([FromForm] CreatePlanDeEntrenamientoDto createPlanDeEntrenamientoDto)
        {
            var userIdClaim = User.Claims.First(c => c.Type == "id");

            var planToCreate = createPlanDeEntrenamientoDto.MapCreatePlanDeEntrnamientoDtoToPlanDeEntrenamiento();
            if (createPlanDeEntrenamientoDto.MediaUrl is not null || createPlanDeEntrenamientoDto.MediaUrl.Length > 0)
            {
                var url = await _cloudMediaService.SubirFotoPerfil(createPlanDeEntrenamientoDto.MediaUrl);
                if (url is not null)
                {
                    planToCreate.MediaUrl = url;
                }
            }
            planToCreate.IdAutorUsuario = int.Parse(userIdClaim.Value);
            try
            {
                await _repository.AddAsync(planToCreate);
                await _repository.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500);
            }

            return Ok();
        }


        //[Authorize]
        //[HttpPut("{id}", Name = "PutPlanDeEntrenamiento")]
        //[ProducesResponseType(201, Type = typeof(PlanDeEntrenamientoDto))]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> PutPlanDeEntrenamiento(int id, [FromBody] PlanDeEntrenamientoDto planDeEntrenamientoDto)
        //{
        //    var plan = Mapper.MapPlanDeEntrenamientoDtoToPlanDeEntrenamiento(planDeEntrenamientoDto);
        //    await _repository.EditAsync(plan, id);
        //    await _repository.SaveChangesAsync();
        //    return NoContent();
        //}

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
