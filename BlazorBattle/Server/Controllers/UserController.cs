using BlazorBattle.Server.Data;
using BlazorBattle.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorBattle.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private int GetUserId() =>  int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        private async Task<User> GetUser() => await _dataContext.Users.FirstOrDefaultAsync(u => u.UserId == GetUserId());

        [HttpGet("getbananas")]
        public async Task<IActionResult> GetBananas()
        {
            var user = await GetUser();          

            return Ok(user.Bananas);
        }


    }
}
