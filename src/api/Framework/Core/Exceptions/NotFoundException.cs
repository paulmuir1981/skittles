using System.Collections.ObjectModel;
using System.Net;

namespace Skittles.Framework.Core.Exceptions;

public class NotFoundException : SkittlesException
{
    public NotFoundException(string message) : base(message, new Collection<string>(), HttpStatusCode.NotFound) { }
}
