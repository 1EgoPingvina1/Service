using Microsoft.EntityFrameworkCore;
using social_network.backend.DTOs;
using social_network.backend.Entities;
using social_network.backend.Interfaces;

namespace social_network.backend.Data.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;

        public PostRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllPosts() => await _context.Posts.ToListAsync();

        public async Task<Post> GetPostByIdAsync(int postId) => await _context.Posts.SingleOrDefaultAsync(i => i.Id == postId);
        

        public async Task <Post> GetPostsByUserIdAsync(int userId)
        => await _context.Posts.FirstOrDefaultAsync(i => i.UserId == userId);
       
        public async Task<bool> DeletePost(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Post> UpdatePost(int id, Post updatePost)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(p => p.Id == id);
            if (post != null)
            {
                post.Title = updatePost.Title;
                post.Description = updatePost.Description;
                await _context.SaveChangesAsync();
                return post;
            }
            throw new ArgumentNullException();
        }

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
    }
}
