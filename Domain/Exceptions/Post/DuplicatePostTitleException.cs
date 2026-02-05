namespace Domain.Exceptions.Post;

public class DuplicatePostTitleException : DomainException
{
    public DuplicatePostTitleException(string message) : base(message)
    {
    }
}