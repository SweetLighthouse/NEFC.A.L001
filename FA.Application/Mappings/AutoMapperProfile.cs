using AutoMapper;
using FA.Application.Dtos.Account;
using FA.Application.Dtos.Blogs;
using FA.Application.Dtos.Users;
using FA.Domain.Entities;

namespace FA.Application.Mappings;

class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        this.CreateMap<User, AccountDto>().ReverseMap();

        this.CreateMap<User, DetailUserDto>().ReverseMap();
        this.CreateMap<User, GeneralUserDto>().ReverseMap();
        this.CreateMap<User, RequestUserDto>().ReverseMap();

        this.CreateMap<Blog, RequestBlogDto>().ReverseMap();
        this.CreateMap<Blog, DetailedResponseBlogDto>().ReverseMap();
        this.CreateMap<Blog, GeneralResponseBlogDto>().ReverseMap();
    }
}
