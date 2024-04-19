using social_network.backend.Entities;
using social_network.backend.Interfaces;

namespace social_network.backend.Data.Repository
{
    public class PostRepository : IPostRepository
    {
        public readonly DataContext _context;

        public PostRepository(DataContext context)
        {
            _context = context;
        }   

        public Task<Post> CreatePostAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> CreateCommentAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCommentAsync(int commentId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePostAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> GetCommentByIdAsync(int commentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetCommentsByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetPostByIdAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Post> UpdatePostAsync(int postId, Post updatedPost)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> UpdatePostAsync(int commentId, Comment updateComment)
        {
            throw new NotImplementedException();
        }
    }
}
