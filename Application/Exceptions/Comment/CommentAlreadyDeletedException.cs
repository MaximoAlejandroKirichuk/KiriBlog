namespace Application.Exceptions.Comment;

public class CommentAlreadyDeletedException(string message) : ApplicationException(message)
{
}
