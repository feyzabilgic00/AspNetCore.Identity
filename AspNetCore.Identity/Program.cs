using AspNetCore.Identity.Abstractions.Token;
using AspNetCore.Identity.Context;
using AspNetCore.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using AspNetCore.Identity.Services.Token;
using TokenHandler = AspNetCore.Identity.Services.Token.TokenHandler;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conString = builder.Configuration.GetConnectionString("MsSql");

builder.Services.AddScoped<ITokenHandler, TokenHandler>();

builder.Services.AddDbContext<CustomDbContext>(options =>
{
    options.UseSqlServer(conString);
});

builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<CustomDbContext>()
    .AddDefaultTokenProviders();  // �ifre s�f�rlama gibi i�lemler i�in

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, // Olu�turulacak token de�erini kimlerin/hangi originlerin/sitelerin kullanaca��n� belirledi�imiz de�erdir. => www.bilgic.com
            ValidateIssuer = true, // Olu�turulacak token de�erini kimin da��tt���n� ifade edece�imiz aland�r. => www.myapi.com
            ValidateLifetime = true, // Olu�turulan token de�erinin s�resini kontrol edecek olan do�rulamad�r.
            ValidateIssuerSigningKey = true, // �retilecek token de�erinin uygulamam�za ait bir de�er oldu�unu ifade eden security key verisinin do�rulanmas�d�r.

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
