using ApiBackend.Context;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    public async Task<IActionResult> AddProduct([FromBody] Product product)
    {
        _context.products.Add(product);
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
    public async Task<IActionResult> UpdateProduct(int id, Product updateProduct)
    {
        var getProduct = await _context.products.FindAsync(id);
        if (getProduct == null)
        {
            return NotFound();
        }

        getProduct.prodId = updateProduct.prodId;
        getProduct.prodName = updateProduct.prodName;
        getProduct.prodDescription = updateProduct.prodDescription;
        getProduct.prodPrice = updateProduct.prodPrice;
        getProduct.prodSertficate = updateProduct.prodSertficate;
        await _context.SaveChangesAsync();
        return Ok(getProduct);
    }
}
