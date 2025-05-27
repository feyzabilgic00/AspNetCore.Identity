namespace AspNetCore.Identity.DTOs;

public class TokenDto
{
    public string AccessToken { get; set; }
    public DateTime Expiration { get; set; }
}
