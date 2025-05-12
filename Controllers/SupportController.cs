using ApiBackend.Context;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupportController : ControllerBase
{

    private readonly AppDbContext _context;

    public SupportController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("GetAllSupport")]
    public async Task<IActionResult> GetAllSupport()
    {
        var getSupport = await _context.supports.ToListAsync();
        return Ok(getSupport);
    }


    [HttpGet("SupportById/{id}")]
    public async Task<IActionResult> GetSupportById(int id)
    {
        var getSupport = await _context.supports.FindAsync(id);
        if (getSupport == null)
        {
            return NotFound();
        }
        return Ok(getSupport);

    }

    [HttpPost("AddSupport")]
    public async Task<IActionResult> AddSupport([FromBody] Support support)
    {
        await _context.supports.AddAsync(support);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("DeleteSupport/{id}")]
    public async Task<IActionResult> DeleteSupport(int id){
        var deleteSupport = await _context.supports.FindAsync(id);
        if(deleteSupport == null) {
            return NotFound();
        }
        _context.supports.Remove(deleteSupport);
        await _context.SaveChangesAsync();
        return Ok(deleteSupport);
    }

}