namespace DataAccess.Utils;

public class AccessorException : Exception
{
    public AccessorException() { }
    public AccessorException(string message): base(message) { }
}