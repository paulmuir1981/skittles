using Skittles.Framework.Core.Exceptions;
using System.Net;

namespace Skittles.WebApi.Domain.Exceptions;

public sealed class PlayerValidationException : SkittlesException
{
    public PlayerValidationException(string fieldName, string message)
        : base($"Validation failed for {fieldName}: {message}", 
               [message], 
               HttpStatusCode.BadRequest)
    { }
}