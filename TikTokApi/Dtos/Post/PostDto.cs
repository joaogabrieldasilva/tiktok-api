namespace TikTokApi.Dtos; 

public class PostDto {
    
    public int Id { get; set; }
    
    public string Description { get; set; }
    
    public UserDto Author { get; set; }
}