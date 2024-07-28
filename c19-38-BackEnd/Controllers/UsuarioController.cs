using c19_38_BackEnd.Dtos;
using c19_38_BackEnd.Interfaces;
using c19_38_BackEnd.Map;
using c19_38_BackEnd.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace c19_38_BackEnd.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IRepository<Usuario> _repository;
        private readonly IRepository<DescripcionObjetivos> _repositoryDesc;
        private readonly UserManager<Usuario> _userManager;
        private readonly ICloudMediaService _cloudMediaService;

        public UsuarioController(IRepository<Usuario> repository,UserManager<Usuario> userManager, ICloudMediaService cloudMediaService,
            IRepository<DescripcionObjetivos> repositoryDesc)
        {
            _repository = repository;
            _userManager = userManager;
            _cloudMediaService = cloudMediaService;
            _repositoryDesc = repositoryDesc;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuario()
        {
            var usuario = await _repository.GetAllAsync();
            if (usuario == null)
            {
                return NotFound();
            }
            var usuariosDto = usuario.Select(e => Mapper.MapUsuarioToUsuarioDto(e)).ToList();
            return Ok(usuariosDto);
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "getUsuario")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuario(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario is null)
            {
                return NotFound();
            }
            var usuarioDto = Mapper.MapUsuarioToUsuarioDto(usuario);
            return Ok(usuarioDto);
        }

        [Authorize]
        [HttpPut("{id}", Name = "PutUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutUsuario(int id, [FromForm] EditUsuarioDto usuarioDto)
        {
            if (!EsEsteElUsuario(User, id))
                return BadRequest();

            var usuarioAEditar = await _repository.GetByIdAsync(id);
            if (usuarioAEditar is null)
            {
                return BadRequest();
            }

            usuarioDto.MapEditUsuarioDtoToUsuario(usuarioAEditar);
            if(usuarioDto.MediaUrl is not null || usuarioDto.MediaUrl.Length >0)
            {
                var url = await _cloudMediaService.SubirFotoPerfil(usuarioDto.MediaUrl);
                if(url is not null)
                {
                    usuarioAEditar.MediaUrl = url;
                }
            }

            var result = await _userManager.UpdateAsync(usuarioAEditar);

            if (!result.Succeeded)
                return StatusCode(500);

            return Ok();
        }
        [Authorize]
        [HttpDelete("{id}", Name = "DeleteUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (!EsEsteElUsuario(User,id))
                return BadRequest();

            var usuarioAEliminar = await _repository.GetByIdAsync(id);
            if (usuarioAEliminar is null)
            {
                return BadRequest();
            }

            var resultIdentity = await _userManager.DeleteAsync(usuarioAEliminar);
            if (!resultIdentity.Succeeded)
                return StatusCode(500);
            return Ok() ;
        }

        [Authorize]
        [HttpPost("usuario/DescripcionObjetivos/{idUsuario}",Name ="PostDescripcionObjetivos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostDescripcion([FromRoute]int idUsuario, [FromBody] DescripcionObjetivosDto descripcion)
        {
            if(!EsEsteElUsuario(User,idUsuario))
                return BadRequest("Id invalido");
            var descripcionObjetivos = descripcion.MapDescripcionObjetivosDtoToDescripcionObjetivos();
            descripcionObjetivos.IdUsuario = idUsuario;
            try
            {
                await _repositoryDesc.AddAsync(descripcionObjetivos);
                await _repository.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500);
            }
            return Ok();

        }
        [NonAction]
        public bool EsEsteElUsuario(ClaimsPrincipal claimsPrincipal,int id)
        {
            var userIdClaim = User.Claims.First(c => c.Type == "id");
            return int.Parse(userIdClaim.Value) == id;
        }
    }
}
