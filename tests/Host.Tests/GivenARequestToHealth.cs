﻿
namespace Skittles.Aspire.Tests;

[TestFixture("/health")]
[TestFixture("/alive")]
public class GivenARequestToHealth : GivenARequestToWebApi
{
    public GivenARequestToHealth(string requestUri)
        : base(requestUri, "text/plain") { }

    [Test]
    public async Task ThenContentIsExpected()
    {
        var text = await _response.Content.ReadAsStringAsync();
        Assert.That(text, Is.EqualTo("Healthy"));
    }
}
