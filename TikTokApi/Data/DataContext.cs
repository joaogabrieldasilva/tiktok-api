using Microsoft.EntityFrameworkCore;
using TikTokApi.Models;

namespace TikTokApi.Data; 

public class DataContext: DbContext {

    public DataContext(DbContextOptions options): base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Post> Posts { get; set; }
    
    public DbSet<PostType> PostTypes { get; set; }
    
    public DbSet<Comment> Comments { get; set; }
    
    public DbSet<CommentAnswer> CommentAnswers { get; set; }
    
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommentAnswer>()
            .HasOne(e => e.Comment)
            .WithMany(e => e.Answers);
        

    }
 
}