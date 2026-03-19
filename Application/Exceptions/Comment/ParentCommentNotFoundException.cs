namespace Application.Exceptions.Comment;

public class ParentCommentNotFoundException(string message) : ApplicationException(message)
{
}

