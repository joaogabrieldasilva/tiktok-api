using Microsoft.EntityFrameworkCore;
using TikTokApi.Data;
using TikTokApi.Interfaces;
using TikTokApi.Models;

namespace TikTokApi.Repositories; 

public class CommentRepository : ICommentRepository {
    private readonly DataContext _context;


    public CommentRepository(DataContext context)
    {
        _context = context;
    }


    public ICollection<Comment> GetCommentsOfAPost(int postId)
    {
        return _context.Comments.Where(c => c.Post.Id == postId).Include(c => c.Author).ToList();
    }

    public Comment GetCommentById(int id)
    {
        return _context.Comments.Where(c => c.Id == id).FirstOrDefault();
    }

    public Comment PostComment(Comment comment)
    {
        var createdPost = _context.Comments.Add(comment).Entity;
        _context.SaveChanges();

        return createdPost;
    }

    public ICollection<CommentAnswer> GetCommentAnswers(int commentId)
    {
        return _context.CommentAnswers.Where(ca => ca.Comment.Id == commentId).Include(ca => ca.Author).ToList();
    }

    public void DeleteComment(Comment comment)
    {
        _context.Comments.Remove(comment);
        _context.SaveChanges();
    }

    public CommentAnswer AnswerComment(CommentAnswer answer)
    {
       var createdAnswer =  _context.CommentAnswers.Add(answer).Entity;
        _context.SaveChanges();

        return createdAnswer;
    }

    public bool CommentExists(int id)
    {
        return _context.Comments.Any(c => c.Id == id);
    }
}