namespace Application.Exceptions.Auth;

public class EmailAlreadyExistException(string message) : ApplicationException(message)
{
    
}