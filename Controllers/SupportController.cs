using ApiBackend.Context;
using ApiBackend.Dto.SupportDto;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
    public async Task<IActionResult> AddSupport([FromBody] AddSupportDto addSupportDto)
    {
        var newSupport = new Support
        {
            messageId = addSupportDto.messageId,
            userId = addSupportDto.userId,
            message = addSupportDto.message,
            messageDate = new DateTime(),
            CDate = new DateTime()
        };
        _context.supports.Add(newSupport);
        await _context.SaveChangesAsync();
        return Ok(newSupport);

    }

    [HttpDelete("DeleteSupport/{id}")]
    public async Task<IActionResult> DeleteSupport(int id)
    {
        var deleteSupport = await _context.supports.FindAsync(id);
        if (deleteSupport == null)
        {
            return NotFound();
        }
        _context.supports.Remove(deleteSupport);
        await _context.SaveChangesAsync();
        return Ok(deleteSupport);
    }

}