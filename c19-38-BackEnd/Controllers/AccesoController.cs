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

namespace c19_38_BackEnd.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly JwtSettings _settings;
        private readonly IRepository<Usuario> _repository;

        public AccesoController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager,
            RoleManager<IdentityRole<int>> roleManager,JwtSettings settings,IRepository<Usuario> repository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _settings = settings;
            _repository = repository;
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
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
        [ProducesResponseType(203)]
        [ProducesResponseType(400,Type =typeof(ProblemDetails))]
        [HttpPost("Registro")]
        public async Task<IActionResult> Registro([FromBody] RegistroDto registroDto)
        {
            var user = Mapper.MapRegistroDtoToUsuario(registroDto);
            var result = await _userManager.CreateAsync(user, registroDto.Contraseña);
            if (!result.Succeeded)
            {
                return StatusCode(500);
            }
            //Añade el rol segun si es entrenador o no.
            var resultado = await _userManager.AddToRoleAsync(user, registroDto.EsEntrenador?Roles.Entrenador:Roles.Atleta);

            if(!resultado.Succeeded)
            {
                return StatusCode(500);
            }

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
        [ProducesResponseType(500)]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(ProblemDetails))]
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

            //Se obtiene el token junto con las claims necesarias
            var tokenJwt = GeneradorDeJWT.GenerarJwt(usuario, roles[0], _settings);

            return Ok(tokenJwt);
        }


        // DESPUES HAY QUE BORRARLO
        [HttpGet("AñadirRoles/{clave}")]
        public async Task<IActionResult> AñadirRoles(int clave)
        {
            //Esta clave es momentanea jaja
            if(clave == 2191)
            {
                await _roleManager.CreateAsync(new IdentityRole<int>
                {
                    Name = Roles.Atleta
                });
                await _roleManager.CreateAsync(new IdentityRole<int>
                {
                    Name = Roles.Entrenador
                });
                return Ok("Roles creados");
            }
            return NotFound("Clave incorrecta");  
        }
    }
}
