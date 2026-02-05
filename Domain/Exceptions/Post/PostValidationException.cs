namespace Domain.Exceptions.Post;

public class PostValidationException:DomainException
{
    public PostValidationException(string message) : base(message)
    {
    }
}