using BlazorBattle.Server.Data;
using BlazorBattle.Server.Services;
using BlazorBattle.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattle.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BattleController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IUtilityService _utilityService;

        public BattleController(DataContext dataContext, IUtilityService utilityService)
        {
            _dataContext = dataContext;
            _utilityService = utilityService;
        }

        [HttpPost]
        public async Task<IActionResult> StartBattle([FromBody] int opponentId)
        {
            var attacker = await _utilityService.GetUser();
            var opponent = await _dataContext.Users.FindAsync(opponentId);

            if(opponent == null || opponent.IsDeleted)
            {
                return NotFound($"{opponent.UserName} is not available. Battle someone else!");
            }

            var result = new BattleResult();

            await Fight(attacker, opponent, result);         
            
            return Ok(result);
        }

        private async Task Fight(User attacker, User opponent, BattleResult result)
        {
            var attackerArmy = await _dataContext.UserUnits
                .Where(u => u.UserId == attacker.UserId && u.HitPoints > 0)
                .Include(u => u.Unit)
                .ToListAsync();

            var opponentArmy = await _dataContext.UserUnits
               .Where(u => u.UserId == opponent.UserId && u.HitPoints > 0)
               .Include(u => u.Unit)
               .ToListAsync();

            var attackerDamageSum = 0;
            var opponentDamageSum = 0;

            int currentRound = 0;

            while(attackerArmy.Count > 0 && opponentArmy.Count > 0)
            {
                currentRound++;

                if(currentRound % 2 != 0)                
                    attackerDamageSum += FightRound(attacker, opponent, attackerArmy, opponentArmy, result);
                else
                    opponentDamageSum += FightRound(opponent, attacker, opponentArmy, attackerArmy, result);
            }

            result.IsVictory = opponentArmy.Count == 0;
            result.RoundsFought = currentRound;

            if (result.RoundsFought > 0)
                await FinishFight(attacker, opponent, result, attackerDamageSum, opponentDamageSum);
          
        }
        private int FightRound(User attacker, User opponent, 
            List<UserUnit> attackerArmy, List<UserUnit> opponentArmy, BattleResult result)
        {
            int randomAttackerIndex = new Random().Next(attackerArmy.Count);
            int randomOpponentIndex = new Random().Next(opponentArmy.Count);

            var randomAttacker = attackerArmy[randomAttackerIndex];
            var randomOpponent = opponentArmy[randomOpponentIndex];

            var damage =
                new Random().Next(randomAttacker.Unit.Attack) - new Random().Next(randomOpponent.Unit.Defense);
           
            if (damage < 0) damage = 0;

            if(damage <= randomOpponent.HitPoints)
            {
                randomOpponent.HitPoints -= damage;
                result.BattleLog.Add(
                    $"{attacker.UserName}'s {randomAttacker.Unit.Title} attacks" +
                    $"{opponent.UserName}'s {randomOpponent.Unit.Title} for {damage} damage!");
            }
            else
            {
                damage = randomOpponent.HitPoints;
                randomOpponent.HitPoints = 0;
                opponentArmy.Remove(randomOpponent);
                result.BattleLog.Add(
                    $"{attacker.UserName}'s {randomAttacker.Unit.Title} kills" +
                    $"{opponent.UserName}'s {randomOpponent.Unit.Title}!");
            }
            return damage;
        }
       
        private async Task FinishFight(User attacker, User opponent, BattleResult result,
           int attackerDamageSum, int opponentDamageSum)
        {
            result.AttackDamageSum = attackerDamageSum;
            result.OpponentDamageSum = opponentDamageSum;
            attacker.Battles++;
            opponent.Battles++;

            if (result.IsVictory)
            {
                attacker.Victories++;
                opponent.Defeats++;
                attacker.Bananas += opponentDamageSum;
                opponent.Bananas += attackerDamageSum * 10;
            }
            else
            {
                attacker.Defeats++;
                opponent.Victories++;
                attacker.Bananas += opponentDamageSum * 10;
                opponent.Bananas += attackerDamageSum;
            }

            await _dataContext.SaveChangesAsync();
        }

    }
}
