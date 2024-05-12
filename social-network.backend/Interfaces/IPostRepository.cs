using Microsoft.AspNetCore.Mvc;
using social_network.backend.DTOs;
using social_network.backend.Entities;

namespace social_network.backend.Interfaces
{
    public interface IPostRepository
    {
        //Для поста
        Task<List<Post>> GetAllPosts();
        Task<Post> GetPostByIdAsync(int postId);
        Task<Post> GetPostsByUserIdAsync(int userId);
        //Task<IEnumerable<Post>> GetPostsFeedAsync(int userId);
        //Task<IEnumerable<Post>> SearchPostsAsync(string searchTerm);
        void CreatePost(Post post);
        Task<bool> DeletePost(int postId);
        Task<Post> UpdatePost(int id, Post postUpdateDTO);

        //Для Комментов
        //Task<Comment> GetCommentById(int commentId);
        //Task<IEnumerable<Comment>> GetCommentsByUserId(int userId);
        ////Task<IEnumerable<Post>> GetCommentsFeedAsync(int userId);
        ////Task<IEnumerable<Post>> SearchCommentAsync(string searchTerm);
        //Task<Comment> CreateComment(Comment comment);
        //Task<Comment> UpdatePost(int commentId, Comment updateComment);
        //Task<bool> DeleteComment(int commentId);
    }
}
