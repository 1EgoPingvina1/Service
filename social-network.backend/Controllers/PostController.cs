using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using social_network.backend.DTOs;
using social_network.backend.Entities;
using social_network.backend.Interfaces;

namespace social_network.backend.Controllers
{

    public class PostController : BaseController
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository,IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<PostForCreateDTO>> CreatePostAsync(PostForCreateDTO post)
        {
            var newpost = _mapper.Map<Post>(post);
            if(newpost != null)
            {
                _postRepository.CreatePost(newpost);
                return Ok(newpost);
            }

            return BadRequest("Post was not created");

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var posts = await _postRepository.GetAllPosts();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var post = await _postRepository.GetPostByIdAsync(id);
                if (post == null)
                    return NotFound("Пост не найден");

                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedPost = await _postRepository.DeletePost(id);
            if (!deletedPost)
                return BadRequest("");

            return Ok(deletedPost);

        }
    }
}