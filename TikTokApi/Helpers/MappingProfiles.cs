using AutoMapper;
using TikTokApi.Dtos;
using TikTokApi.Dtos.Comment;
using TikTokApi.Models;


namespace TikTokApi.Helper; 

public class MappingProfiles : Profile {

    public MappingProfiles()
    {

        CreateMap<Post, PostDto>();
        CreateMap<User, UserDto>();
        CreateMap<Comment, CommentDto>();
        CreateMap<CommentAnswer, CommentAnswerDto>();

    }
}