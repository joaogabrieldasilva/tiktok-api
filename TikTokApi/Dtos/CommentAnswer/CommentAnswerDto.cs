using TikTokApi.Dtos.Comment;

namespace TikTokApi.Dtos; 

public class CommentAnswerDto {
    public int Id { get; set; }
    
    public string Text { get; set; }
    
    public UserDto Author { get; set; }
    
}