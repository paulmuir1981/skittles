using Skittles.Framework.Core.Exceptions;
using System.Net;

namespace Skittles.WebApi.Domain.Exceptions;

public sealed class DuplicateSeasonDescriptionException : SkittlesException
{
    public DuplicateSeasonDescriptionException(Guid seasonId, string description)
        : base($"An event with description '{description}' already exists for season {seasonId}",
               [$"Description '{description}' is already in use for season {seasonId}"],
               HttpStatusCode.Conflict) { }
}