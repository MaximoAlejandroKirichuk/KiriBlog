namespace Application.Exceptions.Comment;

public class CommentNotFoundException(string message) : ApplicationException(message)
{
}

