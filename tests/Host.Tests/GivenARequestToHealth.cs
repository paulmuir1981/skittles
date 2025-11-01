
namespace Skittles.Aspire.Tests;

[TestFixture("/health")]
[TestFixture("/alive")]
public class GivenARequestToHealth : GivenARequestToWebApi
{
    public GivenARequestToHealth(string requestUri)
        : base(requestUri, "text/plain", "Healthy") { }
}
