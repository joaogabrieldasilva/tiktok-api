using Microsoft.EntityFrameworkCore;
using TikTokApi.Models;

namespace TikTokApi.Data; 

public class DataContext: DbContext {

    public DataContext(DbContextOptions options): base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public DbSet<User> Users { get; set; }
 
}