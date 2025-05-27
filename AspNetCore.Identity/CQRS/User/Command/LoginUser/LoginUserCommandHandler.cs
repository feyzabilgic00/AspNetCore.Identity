using AspNetCore.Identity.Abstractions.Token;
using AspNetCore.Identity.DTOs;
using AspNetCore.Identity.Entities;
using AspNetCore.Identity.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Identity.CQRS.User.Command.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    readonly UserManager<AppUser> _userManager;
    readonly SignInManager<AppUser> _signInManager;
    readonly ITokenHandler _tokenHandler;
    public LoginUserCommandHandler(UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager,
        ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
    }
    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
       AppUser? user = await _userManager.FindByNameAsync(request.UsernameOrEmail);

        if (user == null)
            user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

        if (user == null)
           throw new NotFoundUserException();

       SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (result.Succeeded)
        {
            TokenDto token = _tokenHandler.CreateAccessToken(5);
            return new()
            {
                Token = token
            };
        }

        return new()
        {
            Message = "Kullanıcı adı veya şifre yanlış"
        };
    }
}
