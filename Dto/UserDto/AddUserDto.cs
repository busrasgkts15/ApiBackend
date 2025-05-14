namespace ApiBackend.Dto.UserDto;
public class AddUserDto
{
    public string name { get; set; }
    public string surname { get; set; }
    public string e_mail { get; set; }
    public string phone { get; set; }
    public string passwordHash { get; set; }
    public int role { get; set; }

}
