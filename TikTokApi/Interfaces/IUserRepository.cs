using TikTokApi.Models;

namespace TikTokApi.Interfaces; 


public interface IUserRepository {

    public User GetUserById(int id);
    
    public User GetUserByUsername(string username);

    public User SaveUser(User user);

    bool UserExists(string username);

}