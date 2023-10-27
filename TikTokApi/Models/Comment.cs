namespace TikTokApi.Models; 

public class Comment {

    public int Id { get; set; }
    
    public string Text { get; set; }
    
    public User Author { get; set; }
    public Post Post { get; set; }
    
    public ICollection<CommentAnswer> Answers { get; set; }
    
}