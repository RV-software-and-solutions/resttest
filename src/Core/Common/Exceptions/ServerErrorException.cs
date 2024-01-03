namespace RestTest.Core.Common.Exceptions;
public class ServerErrorException : Exception
{
    public ServerErrorException() : base() { }

    public ServerErrorException(string? message) : base(message)
    {
    }

    public ServerErrorException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
