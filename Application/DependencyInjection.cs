using Application.UseCases.Auth.Login;
using Application.UseCases.Auth.Register;
using Application.UseCases.Post.CreatePost;
using Application.UseCases.Post.GetAllPostPublic;
using Application.UseCases.Post.GetPostByIdPublic;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<IRegisterVisitorUseCase, RegisterVisitorUseCase>();
        services.AddScoped<IGetAllPostPublicUseCase, GetAllPostPublicUseCase>();
        services.AddScoped<ICreatePostUseCase, CreatePostUseCase>();
        services.AddScoped<IGetPostByIdPublicUseCase, GetPostByIdPublicUseCase>();
        return services;
    }
}