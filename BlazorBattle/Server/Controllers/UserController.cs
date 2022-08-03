using BlazorBattle.Server.Data;
using BlazorBattle.Server.Services;
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
        private readonly IUtilityService _utilityService;

        public UserController(DataContext dataContext, IUtilityService utilityService)
        {
            _dataContext = dataContext;
            _utilityService = utilityService;
        }

        [HttpGet("getbananas")]
        public async Task<IActionResult> GetBananas()
        {
            var user = await _utilityService.GetUser();

            return Ok(user.Bananas);
        }

        [HttpPut("addbananas")]
        public async Task<IActionResult> AddBananas([FromBody] int bananas)
        {
            var user = await _utilityService.GetUser();

            user.Bananas += bananas;

            await _dataContext.SaveChangesAsync();

            return Ok(user.Bananas);
        }

        [HttpGet("leaderboard")]
        public async Task<IActionResult> GetLeaderboard()
        {
            var users = await _dataContext.Users.Where(user => !user.IsDeleted && user.IsConfirmed).ToListAsync();
            users = users
                .OrderByDescending(u => u.Victories)
                .ThenBy(u => u.Defeats)
                .ThenBy(u => u.DateCreated)
                .ToList();
            int rank = 1;
            var response = users.Select(user => new UserStatistics
            {
                Rank = rank++,
                UserId = user.UserId,
                Username = user.UserName,
                Battles = user.Battles,
                Victories = user.Victories,
                Defeats = user.Defeats
            });

            return Ok(response);
        }

        [HttpPost("revive")]
        public async Task<IActionResult> ReviveArmy()
        {
            var user = await _utilityService.GetUser();
            var userUnits = await _dataContext.UserUnits
                .Where(unit => unit.UserId == user.UserId)
                .Include(unit => unit.Unit)
                .ToListAsync();

            int reviveCost = 1000;

            if (user.Bananas < reviveCost)
            {
                return BadRequest("Not enough Bananas! You need 1000 Bananas to revive your army! ");
            }

            bool armyAlreadyAlive = true;

            foreach (var userUnit in userUnits)
            {
                if (userUnit.HitPoints <= 0)
                {
                    armyAlreadyAlive = false;
                    userUnit.HitPoints = new Random().Next(0, userUnit.Unit.HitPoints);
                }
            }
            if (armyAlreadyAlive)
                return Ok("Your army is already alive!");
            user.Bananas -= reviveCost;
            await _dataContext.SaveChangesAsync();
            return Ok("Some of your army has revived!");
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var user = await _utilityService.GetUser();
            var battles = await _dataContext.Battles
                .Where(battle => battle.AttackerId == user.UserId || battle.OpponentId == user.UserId)
                .Include(battle => battle.Attacker)
                .Include(battle => battle.Opponent)
                .Include(battle => battle.Winner)
                .ToListAsync();
            var history = battles.Select(battle => new BattleHistoryEntry
            {
                BattleId = battle.Id,
                AttackerId = battle.AttackerId,
                OpponenetId = battle.OpponentId,
                YouWon = battle.WinnerId == user.UserId,
                AttackerName = battle.Attacker.UserName,
                OpponentName = battle.Opponent.UserName,
                RoundsFought = battle.RoundsFought,
                BattleDate = battle.BattleDate,
                WinnderDamageDealt = battle.WinnerDamage
            });
            return Ok(history.OrderByDescending(h => h.BattleDate));
        }
    }
}
