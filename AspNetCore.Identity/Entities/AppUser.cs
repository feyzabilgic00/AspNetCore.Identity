using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Identity.Entities;

public class AppUser: IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
}
