using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Domain;

namespace MyWebApi.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetPostsAysnc();
        Task<bool> CreatePostAsync(Post post);
        Task<Post> GetPostByIdAsync(Guid postId);
        Task<bool> UpdatePostAsync(Post postToUpdate);
        Task<bool> DeletePostAsync(Guid postId);
    }
}
