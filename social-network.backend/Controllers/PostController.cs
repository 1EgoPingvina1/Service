using Microsoft.AspNetCore.Mvc;
using social_network.backend.DTOs;
using social_network.backend.Entities;
using social_network.backend.Interfaces;

namespace social_network.backend.Controllers
{

    public class PostController : BaseController
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Post yourDTO)
        {
            try
            {
                var createdPost = await _postRepository.CreatePost(yourDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdPost.Id }, createdPost);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PostForUpdateDTO PostUpdateDTO)
        {
            try
            {
                var updatedPost = await _postRepository.UpdatePost(id, PostUpdateDTO);
                if (updatedPost == null)
                    return NotFound("Пост не найден");

                return Ok(updatedPost);
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
                return BadRequest("Ошибка удаления...");

            return Ok(deletedPost);

        }
    }
}