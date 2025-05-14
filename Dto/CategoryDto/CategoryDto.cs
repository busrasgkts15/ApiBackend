using System.ComponentModel.DataAnnotations;
using Abstract;

namespace ApiBackend.Dto.CategoryDto;
public class CategoryDto
{
    public int categoryId { get; set; }
    public string categoryName { get; set; }
    public int totalProduct { get; set; }
}