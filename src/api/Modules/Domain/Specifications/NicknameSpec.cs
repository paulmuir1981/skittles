using Ardalis.Specification;

namespace Skittles.WebApi.Domain;

public sealed class NicknameSpec : Specification<Player>
{
    public NicknameSpec(string nickname)
    {
        Query
            .Where(p => p.Nickname.ToLower() == nickname.ToLower());
    }
}