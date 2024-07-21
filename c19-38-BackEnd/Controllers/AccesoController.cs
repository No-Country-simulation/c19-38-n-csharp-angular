using c19_38_BackEnd.Configuracion;
using c19_38_BackEnd.Dtos;
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

        public AccesoController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager,
            RoleManager<IdentityRole<int>> roleManager,JwtSettings settings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _settings = settings;
        }

        [HttpPost("Registro")]
        public async Task<IActionResult> Registro([FromBody] RegistroDto registroDto)
        {
            var user = new Usuario
            {
                Nombre = registroDto.Nombre,
                Apellido = registroDto.Apellido,
                Altura = registroDto.Altura,
                Peso = registroDto.Peso,
                FechaDeNac = registroDto.FechaNacimiento,
                Disciplina = registroDto.Disciplina,
                Genero = registroDto.Genero,
                Email = registroDto.Email,
                UserName = registroDto.Nombre,
            };
            var result = await _userManager.CreateAsync(user, registroDto.Contraseña);
            if (!result.Succeeded)
            {
                return StatusCode(500);
            }
            var resultado = await _userManager.AddToRoleAsync(user, registroDto.EsEntrenador?Roles.Entrenador:Roles.Atleta);

            if(!resultado.Succeeded)
            {
                return StatusCode(500);
            }


            return NotFound();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var usuario = await _userManager.FindByEmailAsync(loginDto.Email);
            var result = await _signInManager.PasswordSignInAsync(usuario, loginDto.Contraseña, false, false);
            if (!result.Succeeded)
            {
                return StatusCode(500);
            }

            var roles = await _userManager.GetRolesAsync(usuario);
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







        /// PRUEBA DE AUTENTICACION Y AUTORIZACION.



        [HttpGet("Get"),Authorize(Policy ="Entrenador")]
        public IActionResult Get()
        {
            return Ok("Sos entrenador");
        }

        [HttpGet("GetAtleta"),Authorize(Policy ="Atleta")]
        public IActionResult GetAtleta()
        {
            return Ok("Sos Atleta");
        }

        [HttpGet("EstoyAurizado"),Authorize]
        public IActionResult EstoyAutorizado()
        {
            return Ok(true);
        }
    }
}
