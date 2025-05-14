using System.ComponentModel.DataAnnotations;
using Abstract;

namespace Entities;
public class User : Audit
{
    [Key]   // User Varlığını benzersiz kılan primary key
    public int userId { get; set; }
    //Data annotations ile girilen veriye kısıtlar getirildi.
    [MaxLength(30)]
    public string? name { get; set; } = string.Empty;
    [Required, MaxLength(30)]
    public string surname { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string e_mail { get; set; } = string.Empty;
    [Phone]
    public string? phone { get; set; } = string.Empty;
    [Required, MinLength(6), MaxLength(30)]
    public string passwordHash { get; set; } = string.Empty;
    public ICollection<UserRole>? userRoles { get; set; }

}
