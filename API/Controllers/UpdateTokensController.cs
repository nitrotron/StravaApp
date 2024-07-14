using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateTokensController(DataContext context) : ControllerBase
    {
        // [HttpGet]
        // public async Task<ActionResult<bool>> Auth()
        // {
        //     List<Oauth> oauths = await context.Oauths.ToListAsync();
        //     if (oauths.Count != 1)
        //     {
        //         throw new Exception("wrong number oAuth rows");
        //     }
        //     Oauth oauth = oauths[1];


        // }

    }
}
