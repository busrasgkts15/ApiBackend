
using Entities;

namespace ApiBackend.Authentication;

public class AuthResponse
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }
}
