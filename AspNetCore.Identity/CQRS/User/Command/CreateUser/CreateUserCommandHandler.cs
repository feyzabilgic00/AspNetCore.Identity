using AspNetCore.Identity.Entities;
using AspNetCore.Identity.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Identity.CQRS.User.Command.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    readonly UserManager<AppUser> _userManager;
    public CreateUserCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
       IdentityResult result = await _userManager.CreateAsync(new()
        {
            UserName = request.Username,
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
        }, request.Password);

        CreateUserCommandResponse response = new()
        {
            Successed = result.Succeeded
        };

        if (result.Succeeded)
            response.Message = "Kullanıcı başarıyla oluşturuldu";
        else
        {
            foreach (var error in result.Errors)
            {
                response.Message += $"{error.Code} - {error.Description}<br>";
            }
        }
        return response;
    }
}
