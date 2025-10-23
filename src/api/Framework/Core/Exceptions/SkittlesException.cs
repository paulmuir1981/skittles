using System.Net;

namespace Skittles.Framework.Core.Exceptions;

public class SkittlesException : Exception
{
    public IEnumerable<string> ErrorMessages { get; }

    public HttpStatusCode StatusCode { get; }

    public SkittlesException(
        string message, IEnumerable<string> errors, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
    }

    public SkittlesException(string message) : base(message)
    {
        ErrorMessages = [];
    }
}
