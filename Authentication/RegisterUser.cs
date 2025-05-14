namespace ApiBackend.Authentication;

public class RegisterUser
{
    public int Id { get; set; }
    public string? Surname { get; set; }
    public string? UserName { get; set; }

    public string? e_mail { get; set; }
    public string? Password { get; set; }
    public int UserRole { get; set; }

}