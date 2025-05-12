using ApiBackend.Context;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var getUser = await _context.users.ToListAsync();
        return Ok(getUser);
    }

    [HttpGet("GetUserById/{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var getUser = await _context.users.FindAsync(id);
        if (getUser == null)
        {
            return NotFound();
        }
        return Ok(getUser);
    }

    [HttpPost("AddUser")]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        await _context.users.AddAsync(user);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("DeleteUser/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        _context.users.Remove(user);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("ChangeUser/{id}")]
    public async Task<IActionResult> ChangeUser(int id, User user)
    {
        var changeUser = await _context.users.FindAsync(id);
        if (changeUser == null)
        {
            return NotFound();
        }

        changeUser.name = user.name;
        changeUser.surname = user.surname;
        changeUser.phone = user.phone;
        changeUser.e_mail = user.e_mail;
        await _context.SaveChangesAsync();
        return Ok(changeUser);
    }
}