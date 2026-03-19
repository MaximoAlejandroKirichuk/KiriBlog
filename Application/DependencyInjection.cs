using Application.UseCases.Auth.Login;
using Application.UseCases.Auth.Register;
using Application.UseCases.Comments.CreateComment;
using Application.UseCases.Comments.DeleteComment;
using Application.UseCases.Comments.GetCommentsByPost;
using Application.UseCases.Comments.GetRepliesByCommentId;
using Application.UseCases.Comments.ReplyToComment;
using Application.UseCases.Post.CreatePost;
using Application.UseCases.Post.GetAllPostPublic;
using Application.UseCases.Post.GetPostByIdPublic;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //auth
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<IRegisterVisitorUseCase, RegisterVisitorUseCase>();
        //posts
        services.AddScoped<IGetAllPostPublicUseCase, GetAllPostPublicUseCase>();
        services.AddScoped<ICreatePostUseCase, CreatePostUseCase>();
        services.AddScoped<IGetPostByIdPublicUseCase, GetPostByIdPublicUseCase>();
        //comments
        services.AddScoped<ICreateCommentUseCase, CreateCommentUseCase>();
        services.AddScoped<IReplyToCommentUseCase, ReplyToCommentUseCase>();
        services.AddScoped<IGetCommentsByPostUseCase, GetCommentsByPostUseCase>();
        services.AddScoped<IGetRepliesByCommentIdUseCase, GetRepliesByCommentIdUseCase>();
        services.AddScoped<IDeleteCommentUseCase, DeleteCommentUseCase>();
        return services;
    }
}