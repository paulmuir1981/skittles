using AutoFixture;
using Skittles.WebApi.Domain;

namespace Skittles.Server.Tests.Builders;

public class PlayerBuilder
{
    private string? _name;
    private string? _nickname;
    private bool? _canDrive;
    private bool? _isDeleted;

    public Player Build()
    {
        var fixture = new Fixture();
        return Player.Create(_name ?? "I am a fake name", _nickname, _canDrive ?? false, _isDeleted ?? false);
    }

    public PlayerBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public PlayerBuilder WithNickname(string nickname)
    {
        _nickname = nickname;
        return this;
    }

    public PlayerBuilder WithCanDrive(bool canDrive)
    {
        _canDrive = canDrive;
        return this;
    }

    public PlayerBuilder WithIsDeleted(bool isDeleted)
    {
        _isDeleted = isDeleted;
        return this;
    }
}
