using TikTokApi.Models;

namespace TikTokApi.Interfaces; 

public interface IPostRepository {

    ICollection<Post> GetPostsByUserId(int userId);

    Post GetPostById(int id);
    Post CreatePost(Post post);

    void DeletePost(Post post);

    PostType GetPostTypeById(int postTypeId);

    bool PostExists(int id);
}