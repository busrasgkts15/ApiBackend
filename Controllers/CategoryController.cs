using ApiBackend.Context;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ApiBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
// url adresi tanımlar, [controller], sınıf adının sonundaki controller ekinin kaldırılmış hali ile kullanılır.
public class CategoryController : ControllerBase
{
    // Dependency Incejtion (DI) ile veritabanı bağlantı ve sorguları gerçekleştirmek için :
    private readonly AppDbContext _context;
    public CategoryController(AppDbContext context)
    {
        _context = context;
    }

    //İsteklerin oluşturulma kısmı :
    [HttpGet("GetAllCategories")]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _context.categories.ToListAsync();
        return Ok(categories);
    }

    [HttpGet("GetCategoryById/{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var getCategory = await _context.categories.FindAsync(id);
        if (getCategory == null)
        {
            return NotFound();
        }
        return Ok(getCategory);
    }

    [HttpPost("AddCategory")]
    public async Task<IActionResult> AddCategory([FromBody] Category category)
    {
        _context.categories.Add(category);
        await _context.SaveChangesAsync();
        return Created();

    }

    [HttpDelete("DeleteCategory/{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var deleteCategory = await _context.categories.FindAsync(id);
        if (deleteCategory == null)
        {
            return NotFound();
        }

        _context.categories.Remove(deleteCategory);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("ChangeCategory/{id}")]
    public async Task<IActionResult> ChangeCategory(int id, Category category)
    {
        var changeCategory = await _context.categories.FindAsync(id);
        if (changeCategory == null)
        {
            return NotFound();
        }

        changeCategory.categoryId = category.categoryId;
        changeCategory.categoryName = category.categoryName;
        changeCategory.totalProduct = category.totalProduct;
        await _context.SaveChangesAsync();
        return Ok(changeCategory);
    }


}