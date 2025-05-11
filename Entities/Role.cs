using System.ComponentModel.DataAnnotations;
using Abstract;

namespace Entities;
// Role sınıfı kullanıcıya ait rolleri tanımlar örn: Admin , user gibi.
public class Role : Audit
{
    [Key]
    public int roleId { get; set; }
    [Required]
    public string roleName { get; set; } = string.Empty;
    public ICollection<UserRole> userRoles { get; set; }

}