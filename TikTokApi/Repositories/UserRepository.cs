using TikTokApi.Data;
using TikTokApi.Interfaces;
using TikTokApi.Models;

namespace TikTokApi.Repositories; 

public class UserRepository: IUserRepository {
    private readonly DataContext _context;
    
    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public User GetUserById(int id)
    {
        return _context.Users.Where(u => u.Id == id).FirstOrDefault();
    }
    public User GetUserByUsername(string username)
    {
        return _context.Users.Where(u => u.Username == username).FirstOrDefault();
    }

    public User SaveUser(User user)
    {
        var newUser = _context.Users.Add(user).Entity;
        _context.SaveChanges();

        return newUser;
    }

    public bool UserExists(string username)
    {
        return _context.Users.Any(u => u.Username == username);
    }
}