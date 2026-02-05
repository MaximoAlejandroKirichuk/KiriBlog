using Application.UseCases.Auth.Login;
using Application.UseCases.Post.CreatePost;
using Application.UseCases.Post.GetAllPostPublic;
using Domain.Interface.Common.Security;
using Domain.Interface.Repository;
using Infrastructure.Repositories.Post;
using Infrastructure.Repositories.User;
using Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreatePostUseCase>();
        services.AddScoped<GetAllPostPublicUseCase>();
        services.AddScoped<LoginUseCase>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        return services;
    }
}