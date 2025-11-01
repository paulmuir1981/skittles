
namespace Skittles.Aspire.Tests;

public abstract class GivenARequestToWebApi : HostTests
{
    private GivenARequestToWebApi(
        string? requestUri, HttpStatusCode expectedCode, string? expectedMediaType, string? expectedContent)
        : base("webapi", requestUri, expectedCode, expectedMediaType, expectedContent) { }

    protected GivenARequestToWebApi(string? requestUri, string expectedMediaType, string? expectedContent = null)
        : this(requestUri, HttpStatusCode.OK, expectedMediaType, expectedContent) { }

    protected GivenARequestToWebApi(string? requestUri) 
        : this(requestUri, HttpStatusCode.NotFound, null, null) { }
}
