using AutoMapper;
using FA.Application.Dtos.Accounts;
using FA.Application.Dtos.Users;

//using FA.Application.Dtos.Blogs;
//using FA.Application.Dtos.Categories;
//using FA.Application.Dtos.Comments;
//using FA.Application.Dtos.Posts;
//using FA.Application.Dtos.Tags;
//using FA.Application.Dtos.Users;
using FA.Domain.Entities;

namespace FA.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<LoginDto, User>();
        CreateMap<RegisterDto, User>(); 
        CreateMap<User, AccountDetailDto>(); 

        CreateMap<User, UserIndexDto>();
        CreateMap<User, UserDetailDto>();
        CreateMap<User, UserPreviewDto>();
        CreateMap<UserRequestDto, User>();
        CreateMap<UserUpdateDto, User>();
        CreateMap<UserDetailDto, UserUpdateDto>();
        CreateMap<UserDetailDto, UserChangePasswordViewDto>();
        


        //this.CreateMap<Blog, RequestBlogDto>().ReverseMap();
        //this.CreateMap<Blog, IndexBlogDto>().ReverseMap();
        //this.CreateMap<RequestBlogDto, IndexBlogDto>().ReverseMap();
        //this.CreateMap<Blog, DetailBlogDto>().ForMember(dbo => dbo.PagedPosts, options => options.MapFrom(b => b.Posts)).ReverseMap();


        //this.CreateMap<Category, RequestCategoryDto>().ReverseMap();
        //this.CreateMap<Category, ResponseCategoryDto>().ReverseMap();
        //this.CreateMap<RequestCategoryDto, ResponseCategoryDto>().ReverseMap();

        //this.CreateMap<Comment, RequestCommentDto>().ReverseMap();
        //this.CreateMap<Comment, ResponseCommentDto>().ReverseMap();
        //this.CreateMap<RequestCommentDto, ResponseCommentDto>().ReverseMap();

        //this.CreateMap<Post, RequestPostDto>().ReverseMap();
        //this.CreateMap<Post, IndexPostDto>().ReverseMap();
        //this.CreateMap<RequestPostDto, IndexPostDto>().ReverseMap();

        //this.CreateMap<Tag, RequestTagDto>().ReverseMap();
        //this.CreateMap<Tag, ResponseTagDto>().ReverseMap();
        //this.CreateMap<RequestTagDto, ResponseTagDto>().ReverseMap();

        //this.CreateMap<User, RequestUserDto>().ReverseMap();
        //this.CreateMap<User, ResponseUserDto>().ReverseMap();
        //this.CreateMap<RequestUserDto, ResponseUserDto>().ReverseMap();
    }
}
