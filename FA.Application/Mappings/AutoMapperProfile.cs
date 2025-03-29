using AutoMapper;
using FA.Application.Dtos.AccountDtos;
using FA.Application.Dtos.BlogDtos;
using FA.Application.Dtos.CategoryDtos;
using FA.Application.Dtos.PostDtos;
using FA.Application.Dtos.TagDtos;
using FA.Application.Dtos.Users;
using FA.Domain.Entities;

namespace FA.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Detail to Request map is used in WebApp project, BaseEntityController,
        // method Edit

        CreateMap<LoginDto, User>();
        CreateMap<RegisterDto, User>();
        CreateMap<User, AccountDetailDto>();

        CreateMap<User, UserIndexDto>();
        CreateMap<User, UserDetailDto>();
        CreateMap<User, UserPreviewDto>();
        CreateMap<UserCreateDto, User>();
        CreateMap<UserDetailDto, UserUpdateDto>();
        CreateMap<UserUpdateDto, User>();

        CreateMap<Blog, BlogIndexDto>();
        CreateMap<Blog, BlogDetailDto>();
        CreateMap<BlogCreateDto, Blog>();
        CreateMap<BlogDetailDto, BlogUpdateDto>();
        CreateMap<BlogUpdateDto, Blog>();

        CreateMap<Category, CategoryIndexDto>();
        CreateMap<Category, CategoryDetailDto>();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryDetailDto, CategoryUpdateDto>();
        CreateMap<CategoryUpdateDto, Category>();

        CreateMap<Tag, TagIndexDto>();
        CreateMap<Tag, TagDetailDto>();
        CreateMap<TagCreateDto, Tag>();
        CreateMap<TagDetailDto, TagUpdateDto>();
        CreateMap<TagUpdateDto, Tag>();

        CreateMap<Post, PostIndexDto>()
            //.ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
            //.ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
            ;

        CreateMap<Post, PostDetailDto>();
        CreateMap<PostCreateDto, Post>();
        CreateMap<PostDetailDto, PostUpdateDto>()
            .ForMember(
                request => request.TagNames,
                options => options.MapFrom(
                    detail => detail.Tags.Select(tag => tag.Name).ToList()
                )
            )
            .ForMember(
                request => request.CategoryIds,
                options => options.MapFrom(
                    detail => detail.Categories.Select(category => category.Id).ToList()
                )
            );
        CreateMap<PostUpdateDto, Post>();

    }
}
