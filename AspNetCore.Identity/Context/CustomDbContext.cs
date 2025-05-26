using AspNetCore.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Identity.Context;

public class CustomDbContext: IdentityDbContext<AppUser, AppRole, string>
{
    public CustomDbContext(DbContextOptions<CustomDbContext> options)
    : base(options)
    {
    }
    public DbSet<AppUser> Users { get; set; }

}
