using System.ComponentModel.DataAnnotations;
using Abstract;

namespace Entities;
public class Category : Audit
{
    [Key]
    public int categoryId { get; set; }

    [Required, MaxLength(100)]
    public string categoryName { get; set; } = string.Empty;
    public int? totalProduct { get; set; }
}