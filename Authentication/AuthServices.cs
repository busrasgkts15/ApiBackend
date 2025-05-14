using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiBackend.Context;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ApiBackend.Authentication;

public class AuthServices
{

    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthServices(IConfiguration configuration, AppDbContext context)
    {
        _configuration = configuration;
        _context = context;

    }
    public async Task<string> CreateToken(User user)
    {
        // token üretirken apsetting de yazdığım key'den üretim yapar.
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
                    // new Claim(ClaimTypes.Role, user.UserRoles.ToString()),
                    // Add more claims as needed
                }),
            Expires = DateTime.UtcNow.AddHours(6),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"], // Add this line
            Audience = _configuration["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<AuthResponse> Login(LoginUser loginUser)
    {
        var currentUser = await _context.users.Where(p => p.name == loginUser.name && p.passwordHash == loginUser.passwordHash).FirstOrDefaultAsync();
        if (currentUser == null)
        {
            throw new ApplicationException("Kullanıcı adı ya da şifre hatalı");
        }
        else
        {
            var token = await CreateToken(currentUser);
            var authResponse = new AuthResponse
            {
                Token = token,
                Username = currentUser.name,
                Id = currentUser.userId
            };
            return authResponse;
        }



    }

    public async Task<string> Register(RegisterUser registerUser)
    {
        var user = new User
        {
            userId = registerUser.Id,
            name = registerUser.UserName,
            surname = registerUser.Surname,
            passwordHash = registerUser.Password,
            e_mail = registerUser.e_mail,
            CDate = DateTime.Now,
            userRoles = new List<UserRole> {
                new() {roleId = registerUser.UserRole, CDate = DateTime.Now}
            }
        };

        _context.users.Add(user);
        try
        {
            await _context.SaveChangesAsync();
            return "Ekleme Başarılı";
        }
        catch (Exception ex)
        {
            return $"Başarısız {ex.Message}";
        }


    }




}