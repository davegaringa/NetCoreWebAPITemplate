using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Data;
using MyWebApi.Domain;

namespace MyWebApi.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _dataContext;
        public PostService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Post>> GetPostsAysnc()
        {
            return await _dataContext.Posts.ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            return await _dataContext.Posts.SingleOrDefaultAsync(x => x.Id == postId);
        }

        public async Task<bool> CreatePostAsync(Post post)
        {
            _dataContext.Posts.Add(post);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }
        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            _dataContext.Posts.Update(postToUpdate);
            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> DeletePostAsync(Guid postId)
        {
            // No need to do a get/select to delete an item.
            // Just create a new object with the id, attach it and remove that object. Entity Framework will correctly assume the entity and
            // remove it without additional database calls.
            // var post = await GetPostByIdAsync(postId);

            var post = new Post { Id = postId };

            _dataContext.Posts.Attach(post);

            if (post == null)
                return false;

            _dataContext.Posts.Remove(post);
            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;
        }
    }
}
