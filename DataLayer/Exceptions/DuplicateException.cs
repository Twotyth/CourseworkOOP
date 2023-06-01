namespace DataLayer.Exceptions;

public class DuplicateException : Exception
{
    public DuplicateException(string msg) : base(msg)
    {
    }
}