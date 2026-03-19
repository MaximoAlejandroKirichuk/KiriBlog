namespace Application.Exceptions.Comment;

public class CommentForbiddenException(string message) : ApplicationException(message)
{
}

