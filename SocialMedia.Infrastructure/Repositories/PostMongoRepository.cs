using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostMongoRepository : IPostRepository
    {
        private readonly SocialMediaContext _context;
        public PostMongoRepository(SocialMediaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Post>> GetPosts()
        {
            //await Task.Delay(10);
            var posts = await _context.Posts.ToListAsync();
            return posts;
        }

        public async Task<Post> GetPost(int id)
        {

            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            return post;
        }
        public async Task InsertPost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

		public Task<bool> UpdatePost(Post post)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeletePost(int id)
		{
			throw new NotImplementedException();
		}
	}
}
