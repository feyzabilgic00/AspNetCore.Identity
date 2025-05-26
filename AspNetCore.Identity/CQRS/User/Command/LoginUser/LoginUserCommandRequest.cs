using MediatR;

namespace AspNetCore.Identity.CQRS.User.Command.LoginUser
{
    public class LoginUserCommandRequest: IRequest<LoginUserCommandResponse>
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
