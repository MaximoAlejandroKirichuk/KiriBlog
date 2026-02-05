namespace Domain.Exceptions.Post;

public class InvalidLanguageException : DomainException
{
    public InvalidLanguageException(string message) : base(message)
    {
    }
}