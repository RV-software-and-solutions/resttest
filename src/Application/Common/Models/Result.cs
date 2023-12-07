namespace RestTest.Application.Common.Models;
public class Result
{
    internal Result(bool succeeded, IEnumerable<string> errors, string message)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
        Message = message;
    }

    public bool Succeeded { get; init; }

    public string[] Errors { get; init; }

    public string Message { get; init; }

    public static Result Success()
    {
        return new Result(true, Array.Empty<string>(), string.Empty);
    }

    public static Result Success(string message)
    {
        return new Result(true, Array.Empty<string>(), message);
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors, string.Empty);
    }
}
