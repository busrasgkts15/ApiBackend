using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abstract;

namespace Entities;
public class Product : Audit
{
    [Key]
    public int prodId { get; set; }
    public int categoryId { get; set; }
    [Required, MaxLength(70)]
    public string prodName { get; set; } = string.Empty;
    [Required, MaxLength(500)]
    public string prodDescription { get; set; } = string.Empty;
    [Required]
    public Decimal prodPrice { get; set; }
    //Sertifakaya sahip olmayan ürünler olabilir o yüzden null gelebilir bu kısım.
    public string? prodSertficate { get; set; } = string.Empty;
    [ForeignKey("categoryId")]
    public Category category { get; set; }
}