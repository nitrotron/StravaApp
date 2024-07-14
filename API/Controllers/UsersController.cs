using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

[ApiController]
[Route("api/[controller]")] // /api/users
public class UsersController(DataContext context) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await context.Users.ToListAsync();

        return Ok(users);
    }

    [HttpGet("{id:int}")] // /api/users/ID
    public async Task< ActionResult<User>> GetUsers(int id)
    {
        var user = await context.Users.FindAsync(id);

        if (user == null) return NotFound();

        return user;
    }
}
