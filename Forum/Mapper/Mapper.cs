using AutoMapper;
using Forum.DTOs;
using Forum.Entities;

namespace Forum.Mapper;
public class MapperProfile : Profile {
  public MapperProfile() {
    CreateMap<User, RegisterDTO>().ReverseMap();
    CreateMap<User, UpdateUserDTO>().ReverseMap();
    CreateMap<Comment, CreateCommentDTO>().ReverseMap();
    CreateMap<Comment, UpdateCommentDTO>().ReverseMap();
    CreateMap<Post, CreatePostDTO>().ReverseMap();
    CreateMap<Post, UpdatePostDTO>().ReverseMap();
  }
}
