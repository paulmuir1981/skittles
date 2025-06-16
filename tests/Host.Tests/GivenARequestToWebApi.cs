
namespace Skittles.Aspire.Tests;

public abstract class GivenARequestToWebApi : HostTests
{
    private GivenARequestToWebApi(string? requestUri, HttpStatusCode expectedCode, string? expectedMediaType)
        : base("webapi", requestUri, expectedCode, expectedMediaType) { }

    protected GivenARequestToWebApi(string? requestUri, string expectedMediaType)
        : this(requestUri, HttpStatusCode.OK, expectedMediaType) { }

    protected GivenARequestToWebApi(string? requestUri) : this(requestUri, HttpStatusCode.NotFound, null) { }
}
