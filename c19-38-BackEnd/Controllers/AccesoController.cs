using c19_38_BackEnd.Configuracion;
using c19_38_BackEnd.Dtos;
using c19_38_BackEnd.Interfaces;
using c19_38_BackEnd.Map;
using c19_38_BackEnd.Modelos;
using c19_38_BackEnd.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace c19_38_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IRepository<DescripcionObjetivos> _repositoryDesc;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly JwtSettings _settings;
        private readonly IRepository<Usuario> _repository;

        public AccesoController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager,
            RoleManager<IdentityRole<int>> roleManager,JwtSettings settings,IRepository<Usuario> repository,
            IRepository<DescripcionObjetivos> repositoryDesc)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _settings = settings;
            _repository = repository;
            _repositoryDesc = repositoryDesc;
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// </summary>
        /// <remarks>
        /// Este método registra un nuevo usuario basado en los datos proporcionados en el DTO de registro.
        /// </remarks>
        /// <param name="registroDto">Objeto que contiene la información necesaria para registrar al usuario.</param>
        /// <response code="500">Error interno del servidor</response>
        /// <response code="201">El usuario se creó con éxito</response>
        /// <response code="203">El usuario se creó con éxito</response>
        /// <response code="400">Informacion del RegistroDto no valida</response>
        /// <returns>Una acción de resultado HTTP.</returns>
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status203NonAuthoritative)]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type =typeof(ProblemDetails))]
        [HttpPost("Registro")]
        public async Task<IActionResult> Registro([FromBody] RegistroDto registroDto)
        {
            var user = Mapper.MapRegistroDtoToUsuario(registroDto);
            var result = await _userManager.CreateAsync(user, registroDto.Contraseña);
            if (!result.Succeeded)
            {
                return StatusCode(500);
            }
            ////Añade el rol segun si es entrenador o no.
            //var resultado = await _userManager.AddToRoleAsync(user, registroDto.EsEntrenador?Roles.Entrenador:Roles.Aprendiz);

            //if(!resultado.Succeeded)
            //{
            //    return StatusCode(500);
            //}

            return Created();
        }
        /// <summary>
        /// Inicia sesión en el sistema.
        /// </summary>
        /// <remarks>
        /// Este método autentica a un usuario basado en los datos proporcionados en el DTO de inicio de sesión.
        /// </remarks>
        /// <param name="loginDto">Objeto que contiene la información necesaria para autenticar al usuario.</param>
        /// <response code="500">Error interno del servidor</response>
        /// <response code="200">Inicio de sesión exitoso, retorna el token JWT</response>
        /// <response code="400">Información del LoginDto no válida</response>
        /// <returns>Una acción de resultado HTTP.</returns>
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            //Busca al usuario que tenga el email proporcionado
            var usuario = await _userManager.FindByEmailAsync(loginDto.Email);

            //Realiza el login con la contraseña proporcionada
            var result = await _signInManager.PasswordSignInAsync(usuario, loginDto.Contraseña, false, false);
            if (!result.Succeeded)
            {
                return StatusCode(500);
            }

            //Se obtiene el rol del usuario
            var roles = await _userManager.GetRolesAsync(usuario);

            string token;
            if(roles.IsNullOrEmpty())
            {
                token = GeneradorDeJWT.GenerarJwt(usuario,string.Empty, _settings);
            }
            else
            {
                token = GeneradorDeJWT.GenerarJwt(usuario, roles[0], _settings);
            }

            //Se obtiene el token junto con las claims necesarias
            

            return Ok(token);
        }

        [Authorize]
        [HttpPost("descripcionObjetivos", Name = "PostDescripcionObjetivos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostDescripcion([FromBody] DescripcionObjetivosDto descripcion)
        {
            var userIdClaim = int.Parse(User.Claims.First(c => c.Type == "id").Value);
            var descripcionObjetivos = descripcion.MapDescripcionObjetivosDtoToDescripcionObjetivos();
            descripcionObjetivos.IdUsuario = userIdClaim;

            //Busca al usuario que tenga el email proporcionado
            var usuario = await _repository.GetByIdAsync(userIdClaim);
            //Añade el rol segun si es entrenador o no.
            var resultado = await _userManager.AddToRoleAsync(usuario, descripcion.EsEntrenador ? Roles.Entrenador : Roles.Aprendiz);

            if (!resultado.Succeeded)
            {
                return StatusCode(500);
            }

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
        public bool EsEsteElUsuario(ClaimsPrincipal claimsPrincipal, int id)
        {
            var userIdClaim = User.Claims.First(c => c.Type == "id");
            return int.Parse(userIdClaim.Value) == id;
        }
    }
}
