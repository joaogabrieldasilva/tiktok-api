namespace TikTokApi.Models; 

public class CommentAnswer {
    
    public int Id { get; set; }
    
    public string Text { get; set; }
    
    public User Author { get; set; }
    
    public Comment Comment { get; set; }

}