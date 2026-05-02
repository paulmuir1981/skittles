namespace Skittles.WebApi.Application.Pubs.List.v1;

public sealed record PubResponse(
    Guid Id,
    string Name,
    string Postcode,
    bool IsDeleted);
