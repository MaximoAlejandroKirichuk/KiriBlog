namespace Application.Exceptions.Auth;

public class InvalidCredentialsException(string message) : ApplicationException(message)
{
    
}