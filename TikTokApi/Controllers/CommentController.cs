using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TikTokApi.Dtos;
using TikTokApi.Dtos.Comment;
using TikTokApi.Interfaces;
using TikTokApi.Models;

namespace TikTokApi.Controllers; 

[Route("api/comments")]
[ApiController]
public class CommentController: Controller {
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CommentController(ICommentRepository commentRepository, IPostRepository postRepository, IUserRepository userRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _postRepository = postRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }


    [HttpGet("{postId}")]
    [ProducesResponseType(200, Type = typeof(ICollection<CommentDto>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [Authorize]
    public IActionResult GetPostComments(int postId)
    {

        var postExists = _postRepository.PostExists(postId);

        if (!postExists)
            return NotFound("Post not found");

        if (!ModelState.IsValid)
            return BadRequest();
        
        var comments = _mapper.Map<List<CommentDto>>(_commentRepository.GetCommentsOfAPost(postId));
        
        return Ok(comments);
    }
    
    [HttpGet("{commentId}/answers")]
    [ProducesResponseType(200, Type = typeof(ICollection<CommentAnswerDto>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [Authorize]
    public IActionResult GetCommentAnswers(int commentId)
    {

        var commentExists = _commentRepository.CommentExists(commentId);

        if (!commentExists)
            return NotFound("Comment not found");

        if (!ModelState.IsValid)
            return BadRequest();
        
        var commentsAnswers = _mapper.Map<List<CommentAnswerDto>>(_commentRepository.GetCommentAnswers(commentId));
        
        return Ok(commentsAnswers);
    }

    [HttpPost("{postId}")]
    [ProducesResponseType(201, Type = typeof(PostDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [Authorize]
    public IActionResult PostComment(int postId, [FromBody] PostCommentRequestDto body)
    {
        var postExists = _postRepository.PostExists(postId);

        if (!postExists)
            return NotFound("Post not found");

        if (!ModelState.IsValid)
            return BadRequest();

        var post = _postRepository.GetPostById(postId);
        
        var sessionUserId = Int16.Parse(this.User.Claims.First(i => i.Type == "userId").Value);

        var author = _userRepository.GetUserById(sessionUserId);

        var comment = new Comment()
        {
            Text = body.Text,
            Author = author,
            Post = post
        };
        
        var newComment = _commentRepository.PostComment(comment);

        return Ok(_mapper.Map<CommentDto>(newComment));
    }
    
    [HttpPost("{commentId}/answer")]
    [ProducesResponseType(201, Type = typeof(CommentDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [Authorize]
    public IActionResult AnswerComment(int commentId, [FromBody] PostCommentRequestDto body)
    {
        var commentExists = _commentRepository.CommentExists(commentId);

        if (!commentExists)
            return NotFound("Comment not found");

        if (!ModelState.IsValid)
            return BadRequest();

        var comment = _commentRepository.GetCommentById(commentId);
        
        var sessionUserId = Int16.Parse(this.User.Claims.First(i => i.Type == "userId").Value);

        var author = _userRepository.GetUserById(sessionUserId);

        var commentAnswer = new CommentAnswer()
        {
            Author = author,
            Comment = comment,
            Text = body.Text,
            
        };
        
        var createdAnswer = _commentRepository.AnswerComment(commentAnswer);
        
        return Ok(_mapper.Map<CommentAnswerDto>(createdAnswer));
    }
}