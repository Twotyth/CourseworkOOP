namespace Application.Exceptions;

public class InvalidSessionException : Exception
{
    public InvalidSessionException(string msg) : base(msg)
    {
        
    }
}