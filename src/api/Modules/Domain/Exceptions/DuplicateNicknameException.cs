using Skittles.Framework.Core.Exceptions;
using System.Net;

namespace Skittles.WebApi.Domain.Exceptions;

public sealed class DuplicateNicknameException : SkittlesException
{
    public DuplicateNicknameException(string nickname)
        : base($"A player with nickname '{nickname}' already exists", 
               [$"Nickname '{nickname}' is already in use"],
               HttpStatusCode.Conflict)
    { }
}