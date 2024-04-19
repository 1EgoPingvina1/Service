using social_network.backend.Entities;

namespace social_network.backend.Interfaces
{
    public interface IPostRepository
    {
        //Для поста
        Task<Post> GetPostByIdAsync(int postId);
        Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId);
        //Task<IEnumerable<Post>> GetPostsFeedAsync(int userId);
        //Task<IEnumerable<Post>> SearchPostsAsync(string searchTerm);
        Task<Post> CreatePostAsync(Post post);
        Task<Post> UpdatePostAsync(int postId, Post updatedPost);
        Task<bool> DeletePostAsync(int postId);

        //Для Комментов
        Task<Comment> GetCommentByIdAsync(int commentId);
        Task<IEnumerable<Post>> GetCommentsByUserIdAsync(int userId);
        //Task<IEnumerable<Post>> GetCommentsFeedAsync(int userId);
        //Task<IEnumerable<Post>> SearchCommentAsync(string searchTerm);
        Task<Comment> CreateCommentAsync(Comment comment);
        Task<Comment> UpdatePostAsync(int commentId, Comment updateComment);
        Task<bool> DeleteCommentAsync(int commentId);
    }
}
