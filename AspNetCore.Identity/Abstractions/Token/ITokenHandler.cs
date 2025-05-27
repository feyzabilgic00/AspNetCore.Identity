using AspNetCore.Identity.DTOs;

namespace AspNetCore.Identity.Abstractions.Token;

public interface ITokenHandler
{
    TokenDto CreateAccessToken(int minute);
}
