using c19_38_BackEnd.Dtos;
using c19_38_BackEnd.Interfaces;
using c19_38_BackEnd.Map;
using c19_38_BackEnd.Modelos;
using c19_38_BackEnd.Servicios;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace c19_38_BackEnd.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IRepository<Post> _repository;
        private readonly ICloudMediaService _cloudMediaService;

        public PostController(IRepository<Post> repository, ICloudMediaService cloudMediaService)
        {
            _repository = repository;
            _cloudMediaService = cloudMediaService;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPost()
        {
            var post = await _repository.GetAllAsync();
            if (post == null)
            {
                return NotFound();
            }
            var postDto = post.Select(p =>Mapper.MapPostToPostDto(p)).ToList();
            return Ok(postDto);
        }

        [HttpGet("{id}", Name = "getPost")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPost(int id)
        {
            var post = await _repository.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            var postDto = Mapper.MapPostToPostDto(post);
            return Ok(postDto);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("CretePost")]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostDto createPostDto)
        {
            var userIdClaim = User.Claims.First(c => c.Type == "id");

            var postToCreate = createPostDto.MapCreatePostDtoToPost();
            if (createPostDto.MediaUrl is not null || createPostDto.MediaUrl.Length > 0)
            {
                var url = await _cloudMediaService.SubirFotoPerfil(createPostDto.MediaUrl);
                if (url is not null)
                {
                    postToCreate.MediaUrl = url;
                }
            }
            postToCreate.IdAutorUsuario = int.Parse(userIdClaim.Value);
            try
            {
                await _repository.AddAsync(postToCreate);
                await _repository.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500);
            }           

            return Ok();
        }

        //[Authorize]
        //[HttpPut("{id}", Name = "PutPost")]
        //[ProducesResponseType(201, Type = typeof(PostDto))]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Put(int id, [FromBody] PostDto postDto)
        //{
        //    var post = Mapper.MapPostDtoToPost(postDto);
        //    await _repository.EditAsync(post, id);
        //    await _repository.SaveChangesAsync();
        //    return NoContent();
        //}

        [HttpDelete("{idPost}", Name = "DeletePost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePost(int idPost)
        {
            var postToDelete = await _repository.GetByIdAsync(idPost);
            if(postToDelete is null)
            {
                return BadRequest();
            }
            try
            {
                await _repository.DeleteAsync(idPost);
                await _repository.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500);
            }
            return Ok();
            
        }
    }
}
