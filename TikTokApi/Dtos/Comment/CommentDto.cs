namespace TikTokApi.Dtos.Comment; 

public class CommentDto {
    public int Id { get; set; }
    
    public string Text { get; set; }
    
    public UserDto Author { get; set; }

}