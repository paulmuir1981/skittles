
namespace Skittles.Aspire.Tests;

[TestFixture("")]
[TestFixture("/index.html")]
public class GivenARequestToSwagger : GivenARequestToWebApi
{
    public GivenARequestToSwagger(string swaggerSuffix) 
        : base($"/swagger{swaggerSuffix}", "text/html") { }
}
