using Microsoft.EntityFrameworkCore;
using TikTokApi.Data;
using TikTokApi.Interfaces;
using TikTokApi.Models;

namespace TikTokApi.Repositories; 

public class PostRepository: IPostRepository {
    private readonly DataContext _context;

    public PostRepository(DataContext context)
    {
        _context = context;
    }


    public ICollection<Post> GetPostsByUserId(int userId)
    {
        return _context.Posts.Where(p => p.Author.Id == userId).Include(p => p.Author).ToList();
    }

    public Post GetPostById(int id)
    {
        return _context.Posts.Where(p => p.Id == id).Include(p => p.Author).FirstOrDefault();
    }

    public Post CreatePost(Post post)
    {
         var createdPost = _context.Posts.Add(post).Entity;
         _context.SaveChanges();
         return createdPost;
    }

    public void DeletePost(Post post)
    {
        _context.Posts.Remove(post);
        _context.SaveChanges();
    }

    public PostType GetPostTypeById(int postTypeId)
    {
        return _context.PostTypes.Where(pt => pt.Id == postTypeId).FirstOrDefault();
    }

    public bool PostExists(int id)
    {
        return _context.Posts.Any(p => p.Id == id);
    }
}