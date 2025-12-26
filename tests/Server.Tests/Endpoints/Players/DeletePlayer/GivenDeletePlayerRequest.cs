namespace Skittles.Server.Tests.Endpoints.Players.DeletePlayer;

public abstract class GivenDeletePlayerRequest : ServerTestBase
{
    protected Guid _playerId;
    protected HttpResponseMessage? _response;

    protected async Task DeletePlayer(Guid id)
    {
        _response = await HttpClient!.DeleteAsync($"/api/v1/players/{id}");
    }
}