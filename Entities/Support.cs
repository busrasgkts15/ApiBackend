using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abstract;

namespace Entities;

public class Support : Audit
{
    [Key]
    public int messageId { get; set; }
    public int userId { get; set; }
    [Required, MaxLength(500)]
    public string message { get; set; } = string.Empty;
    public bool IsOpen { get; set; }

    // Admin yada mesajı alan kişi tarafından gönderilen mesaj
    [MaxLength(1000)]
    public string? replyMessage { get; set; } = string.Empty;
    public DateTime messageDate { get; set; }

    [ForeignKey("userId")]
    public User user { get; set; }
}