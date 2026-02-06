using Application.Exceptions.Post;
using Application.UseCases.Post.CreatePost.Dtos;
using Domain.Enums;
using Domain.Exceptions.Post;
using Domain.Interface.Repository;

namespace Application.UseCases.Post.CreatePost;

public class CreatePostUseCase
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public CreatePostUseCase(IPostRepository postRepository, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task ExecuteAsync(CreatePostRequest createPostRequest)
    {
        var existByTitle = await _postRepository.ExistByTitle(createPostRequest.Title);
        if(existByTitle)  throw new DuplicatePostTitleException("Title already exists");

        var author = await _userRepository.GetByEmailAsync(createPostRequest.Email);
        if(author == null) throw new AuthorNotFoundException("Author not found");

        if(!author.IsAuthor()) throw new UnauthorizedAuthorException("Author not found");
        
        var post = new Domain.Entities.Post
            (author.Id,
            createPostRequest.Title, 
            createPostRequest.Content, 
            createPostRequest.Visibility,
            createPostRequest.Language );
        
        await _postRepository.CreateAsync(post);
    }
}