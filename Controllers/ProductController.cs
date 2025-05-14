using ApiBackend.Context;
using ApiBackend.Dto.ProductDto;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        var getProducts = await _context.products.ToListAsync();
        return Ok(getProducts);
    }

    [HttpGet("GetProduct/{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var getProduct = await _context.products.FindAsync(id);
        if (getProduct == null)
        {
            return NotFound();
        }
        return Ok(getProduct);
    }

    [HttpPost("AddProduct")]
    public async Task<IActionResult> AddProduct([FromBody] AddProductDto addProductDto)
    {
        var newProduct = new Product
        {
            prodId = addProductDto.prodId,
            categoryId = addProductDto.categoryId,
            prodName = addProductDto.prodName,
            prodDescription = addProductDto.prodDescription,
            prodPrice = addProductDto.prodPrice,
            prodSertficate = addProductDto.prodSertficate,
            CDate = new DateTime()
        };
        _context.products.Add(newProduct);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("DeleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.products.Remove(product);
        await _context.SaveChangesAsync();
        return Ok(product);
    }



    [HttpPut("UpdateProduct/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, UpsertProductDto upsertProductDto)
    {
        var getProduct = await _context.products.Where(p => p.prodId == id).FirstOrDefaultAsync();
        if (getProduct == null)
        {
            return NotFound();
        }

        getProduct.prodName = upsertProductDto.prodName;
        getProduct.prodDescription = upsertProductDto.prodDescription;
        getProduct.prodPrice = upsertProductDto.prodPrice;
        getProduct.prodSertficate = upsertProductDto.prodSertficate;
        await _context.SaveChangesAsync();
        return Ok(getProduct);
    }
}
