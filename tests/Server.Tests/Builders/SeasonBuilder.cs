using Skittles.WebApi.Domain;

namespace Skittles.Server.Tests.Builders;

public class SeasonBuilder
{
    private int? _year;

    public Season Build()
    {
        return Season.Create(_year ?? DateTime.UtcNow.Year);
    }

    public SeasonBuilder WithYear(int year)
    {
        _year = year;
        return this;
    }
}
