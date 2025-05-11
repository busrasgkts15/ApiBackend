using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class UserRole
{
    [Key]
    public int userRoleId { get; set; }
    [Required]
    public int userId { get; set; }
    [Required]
    public int roleId { get; set; }
    public DateTime registrationDate { get; set; }

    [ForeignKey("userId")]
    public User user { get; set; }
    [ForeignKey("roleId")]
    public Role role { get; set; }
}