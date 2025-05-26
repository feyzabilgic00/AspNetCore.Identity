using AspNetCore.Identity.Entities;
using AspNetCore.Identity.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Identity.CQRS.User.Command;

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

        if (result.Succeeded)
            return new()
            {
                Successed = true,
                Message = "Kullanıcı başarıyla oluşturuldu!"
            };

        throw new UserCreateFailedException("Kullanıcı oluşturulamadı");
    }
}
