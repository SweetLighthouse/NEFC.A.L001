using AutoMapper;
using FA.Application.Dtos.Accounts;
using FA.Application.Dtos.Blogs;
using FA.Application.Dtos.Categories;
using FA.Application.Dtos.Comments;
using FA.Application.Dtos.Posts;
using FA.Application.Dtos.Tags;
using FA.Application.Dtos.Users;
using FA.Domain.Entities;
using FA.Domain.Interfaces.Entities;

namespace FA.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        this.CreateMap<User, LoginDto>().ReverseMap();
        this.CreateMap<User, RegisterDto>().ReverseMap();

        this.CreateMap<Blog, RequestBlogDto>().ReverseMap();
        this.CreateMap<Blog, ResponseBlogDto>().ReverseMap();
        this.CreateMap<RequestBlogDto, ResponseBlogDto>().ReverseMap();


        this.CreateMap<Category, RequestCategoryDto>().ReverseMap();
        this.CreateMap<Category, ResponseCategoryDto>().ReverseMap();
        this.CreateMap<RequestCategoryDto, ResponseCategoryDto>().ReverseMap();

        this.CreateMap<Comment, RequestCommentDto>().ReverseMap();
        this.CreateMap<Comment, ResponseCommentDto>().ReverseMap();
        this.CreateMap<RequestCommentDto, ResponseCommentDto>().ReverseMap();

        this.CreateMap<Post, RequestPostDto>().ReverseMap();
        this.CreateMap<Post, ResponsePostDto>().ReverseMap();
        this.CreateMap<RequestPostDto, ResponsePostDto>().ReverseMap();

        this.CreateMap<Tag, RequestTagDto>().ReverseMap();
        this.CreateMap<Tag, ResponseTagDto>().ReverseMap();
        this.CreateMap<RequestTagDto, ResponseTagDto>().ReverseMap();

        this.CreateMap<User, RequestUserDto>().ReverseMap();
        this.CreateMap<User, ResponseUserDto>().ReverseMap();
        this.CreateMap<RequestUserDto, ResponseUserDto>().ReverseMap();
    }
}
