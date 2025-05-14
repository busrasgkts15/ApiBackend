using Entities;

namespace ApiBackend.Dto.SupportDto;

public class AddSupportDto
{
    public int messageId { get; set; }
    public int userId { get; set; }
    public string message { get; set; }
    public DateTime messageDate { get; set; }
}