using ApiBackend.Context;
using ApiBackend.Dto.UserDto;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
    public async Task<IActionResult> AddUser([FromBody] AddUserDto addUserDto)
    {
        var newUser = new User
        {
            name = addUserDto.name,
            surname = addUserDto.surname,
            e_mail = addUserDto.e_mail,
            passwordHash = addUserDto.passwordHash,
            phone = addUserDto.phone,
            CDate = new DateTime(),
        };
        _context.users.Add(newUser);
        await _context.SaveChangesAsync();


        var newUserRole = new UserRole
        {
            userId = newUser.userId,
            roleId = addUserDto.role,

        };

        _context.userRoles.Add(newUserRole);
        await _context.SaveChangesAsync();
        return Ok();

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

    [HttpPut("ChangeUser/{userId}")]
    public async Task<IActionResult> ChangeUser(int userId, UpsertUserDto upsertUserDto)
    {
        var changeUser = await _context.users.Where(p => p.userId == userId).FirstOrDefaultAsync();
        if (changeUser == null)
        {
            return NotFound();
        }

        changeUser.name = upsertUserDto.name;
        changeUser.surname = upsertUserDto.surname;
        changeUser.phone = upsertUserDto.phone;
        changeUser.e_mail = upsertUserDto.e_mail;
        changeUser.passwordHash = upsertUserDto.passwordHash;

        await _context.SaveChangesAsync();
        return Ok(changeUser);
    }
}