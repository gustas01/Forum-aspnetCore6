using AutoMapper;
using Forum.DTOs;
using Forum.Entities;

namespace Forum.Mapper;
public class MapperProfile : Profile {
  public MapperProfile() {
    //CreateMap<Pizza, CreatePizzaDTO>().ReverseMap();
    CreateMap<User, RegisterDTO>().ReverseMap();
    CreateMap<User, UpdateUserDTO>().ReverseMap();
  }
}
