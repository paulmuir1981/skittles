using Skittles.Framework.Core.Exceptions;
using System.Net;

namespace Skittles.WebApi.Domain.Exceptions;

public sealed class DuplicatePubNameException : SkittlesException
{
    public DuplicatePubNameException(string name)
        : base($"A pub with name '{name}' already exists", 
               [$"Name '{name}' is already in use"],
               HttpStatusCode.Conflict) { }
}