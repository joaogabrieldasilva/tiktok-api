using TikTokApi.Models;

namespace TikTokApi.Interfaces; 

public interface ICommentRepository {

    ICollection<Comment> GetCommentsOfAPost(int postId);

    Comment GetCommentById(int id);
    Comment PostComment(Comment comment);

    ICollection<CommentAnswer> GetCommentAnswers(int commentId);

    void DeleteComment(Comment comment);

    CommentAnswer AnswerComment(CommentAnswer answer);

    bool CommentExists(int id);
}