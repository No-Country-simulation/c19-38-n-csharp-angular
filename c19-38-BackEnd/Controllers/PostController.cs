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
    public class PostController : ControllerBase
    {
        private readonly IRepository<Post> _repository;

        public PostController(IRepository<Post> repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Post>>> GetPost()
        {
            var post = await _repository.GetAllAsync();
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpGet("{id}", Name = "getPost")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Post>>> GetPost(int id)
        {
            var post = await _repository.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [Authorize]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
        [ProducesResponseType(203)]
        [ProducesResponseType(400, Type = typeof(ProblemDetails))]
        [HttpPost("RegistroPost")]
        public async Task<IActionResult> RegistroPost([FromBody] PostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var post = Mapper.MapPostDtoToPost(postDto);
                await _repository.AddAsync(post);
                await _repository.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPost), new { id = postDto.IdPost }, post);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving the post to the database");
            }
        }

        [Authorize]
        [HttpPut("{id}", Name = "PutPost")]
        [ProducesResponseType(201, Type = typeof(PostDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] PostDto postDto)
        {
            var post = Mapper.MapPostDtoToPost(postDto);
            await _repository.EditAsync(post, id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeletePost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
