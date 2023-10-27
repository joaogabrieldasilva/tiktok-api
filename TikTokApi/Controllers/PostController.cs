using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TikTokApi.Dtos;
using TikTokApi.Dtos.Post;
using TikTokApi.Interfaces;
using TikTokApi.Models;

namespace TikTokApi.Controllers; 

[Route("api/posts")]
[ApiController]
public class PostController: Controller {
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public PostController(IPostRepository postRepository, IUserRepository userRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet("/user/{userId}")]
    [ProducesResponseType(200, Type =typeof(ICollection<PostDto>))]
    public IActionResult GetUserPosts(int userId)
    {

        var posts = _mapper.Map<List<PostDto>>(_postRepository.GetPostsByUserId(userId));

        if (!ModelState.IsValid)
            return BadRequest();

        return Ok(posts);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(PostDto))]
    [ProducesResponseType(400)]
    public IActionResult CreatePost(
        [FromBody] CreatePostRequestDto request
        )
    {

        if (!ModelState.IsValid)
            return BadRequest();

        var userId = Int16.Parse(this.User.Claims.First(i => i.Type == "userId").Value);

        var authorExists = _userRepository.UserExists(userId);

        if (!authorExists)
            return NotFound("User not found");

        var postAuthor = _userRepository.GetUserById(userId);

        var postType = _postRepository.GetPostTypeById(1);
        
        var newPost = _mapper.Map<PostDto>(_postRepository.CreatePost(new Post() {
            Author = postAuthor,
            Description = request.Description,
            Type = postType
        }));
        

        return Ok(newPost);
    }

    [Authorize]
    [HttpDelete("{postId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult DeletePost(int postId)
    {

        var postExists = _postRepository.PostExists(postId);

        if (!postExists)
            return NotFound("Post not found.");

        if (!ModelState.IsValid)
            return BadRequest();

        var post = _postRepository.GetPostById(postId);
        
        var sessionUserId = Int16.Parse(this.User.Claims.First(i => i.Type == "userId").Value);

        if (post.Author.Id != sessionUserId)
            return Unauthorized("This post doesn't belong to the logged user");
            

        _postRepository.DeletePost(post);
        return Ok();
    }
}