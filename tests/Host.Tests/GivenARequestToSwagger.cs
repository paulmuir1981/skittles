
namespace Skittles.Aspire.Tests;

[TestFixture("")]
[TestFixture("/index.html")]
public class GivenARequestToSwagger : GivenARequestToWebApi
{
    public GivenARequestToSwagger(string swaggerSuffix) : base($"/swagger{swaggerSuffix}","text/html") { }

    [Test]
    public async Task ThenTitleIsExpected()
    {
        var html = await _response!.Content.ReadAsStringAsync();
        Assert.That(html, Does.Contain("<title>Swagger UI</title>"));
    }
}
