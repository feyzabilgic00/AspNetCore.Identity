using AspNetCore.Identity.DTOs;

namespace AspNetCore.Identity.CQRS.User.Command.LoginUser;

public class LoginUserCommandResponse
{
    public TokenDto Token { get; set; }
    public string Message { get; set; }
}
