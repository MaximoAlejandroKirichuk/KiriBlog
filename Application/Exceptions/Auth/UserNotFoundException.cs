namespace Application.Exceptions.Auth;

public class UserNotFoundException(string message) : ApplicationException(message)
{
    
}