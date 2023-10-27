namespace TikTokApi.Models; 

public class Post {

    public int Id { get; set; }
    
    public string Description { get; set; }
    
    public PostType Type { get; set; }
    
    public string? VideoUrl { get; set; }
    
    public User Author { get; set; }
    
    
}