using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTO.Fight;

namespace WebAPI.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FightService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
        {
            var response = new ServiceResponse<FightResultDto>
            {
                Data = new FightResultDto()
            };

            try
            {
                var characters = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Where(c => request.CharacterIds.Contains(c.Id)).ToListAsync();

                if (characters.Count != request.CharacterIds.Count)
                {
                    response.Success = false;
                    response.Message = "One or more characters do not exist";
                    return response;
                }
                bool defeated = false;
                while (!defeated)
                {
                    foreach (var attacker in characters)
                    {
                        var opponents = characters.Where(c => c.Id != attacker.Id).ToList();
                        var opponent = opponents[new Random().Next(opponents.Count)];

                        int damage = 0;
                        string attackUsed = string.Empty;

                        bool useWeapon = new Random().Next(2) == 0;
                        if (useWeapon)
                        {
                            attackUsed = attacker.Weapon.Name;
                            damage = WeaponAttack(attacker, opponent);
                        }
                        else
                        {
                            var skill = attacker.Skills[new Random().Next(attacker.Skills.Count)];
                            attackUsed = skill.Name;
                            damage = UseSkill(attacker, opponent, skill);
                        }
                        response.Data.BattleLog
                            .Add($"{attacker.Name} attacked {opponent.Name} with {attackUsed} for {(damage >= 0 ? damage : 0)} damage!");

                        if (opponent.HitPoints <= 0)
                        {
                            defeated = true;
                            attacker.Victories++;
                            opponent.Defeats++;
                            response.Data.BattleLog.Add($"{opponent.Name} has been defeated!");
                            response.Data.BattleLog.Add($"{attacker.Name} has won the fight with {attacker.HitPoints} HP left!");
                            break;
                        }
                    }
                    characters.ForEach(c =>
                   {
                       c.HitPoints = 100;
                       c.Battles++;
                   });
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _context.Characters
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                var defender = await _context.Characters
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == request.DefenderId);

                if (attacker == null || defender == null)
                {
                    response.Success = false;
                    response.Message = "One of the characters does not exist";
                    return response;
                }
                var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);
                if (skill == null)
                {
                    response.Success = false;
                    response.Message = $"{attacker.Name} doen't know that skill!";
                    return response;
                }

                int damage = UseSkill(attacker, defender, skill);

                if (defender.HitPoints <= 0)
                    response.Message = $"{defender.Name} has been killed";

                await _context.SaveChangesAsync();
                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Defender = defender.Name,
                    AttackerHP = attacker.HitPoints,
                    DefenderHp = defender.HitPoints,
                    Damage = damage
                };

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        private static int UseSkill(Character? attacker, Character? defender, Skill? skill)
        {
            int damage = skill.Damage + (new Random().Next(attacker.Intelligence));
            damage -= new Random().Next(defender.Defense);

            if (damage > 0)
                defender.HitPoints -= damage;
            return damage;
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                var defender = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.DefenderId);
                if (attacker == null || defender == null)
                {
                    response.Success = false;
                    response.Message = "One of the characters does not exist";
                    return response;
                }
                if (attacker.Weapon == null || defender.Weapon == null)
                {
                    response.Success = false;
                    response.Message = "One of the characters does not have a weapon";
                    return response;
                }
                int damage = WeaponAttack(attacker, defender);

                if (defender.HitPoints <= 0)
                    response.Message = $"{defender.Name} has been killed";
                await _context.SaveChangesAsync();
                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Defender = defender.Name,
                    AttackerHP = attacker.HitPoints,
                    DefenderHp = defender.HitPoints,
                    Damage = damage
                };

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        private static int WeaponAttack(Character? attacker, Character? defender)
        {
            int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
            damage -= new Random().Next(defender.Defense);

            if (damage > 0)
                defender.HitPoints -= damage;
            return damage;
        }

        public async Task<ServiceResponse<List<HighScoreDto>>> GetHighScore()
        {
             var response = new ServiceResponse<List<HighScoreDto>>();
            var characters = await _context.Characters
                .Where(c => c.Battles > 0)
                .OrderByDescending(c => c.Victories)
                .ThenBy(c => c.Defeats)
                .ToListAsync();
            if (characters == null)
            {
                response.Success = false;
                response.Message = "No characters found";
                return response;
            }
            response = new ServiceResponse<List<HighScoreDto>>
            {
                Data = characters.Select(c => _mapper.Map<HighScoreDto>(c)).ToList()
            };
        
            return response;
        }
    }
}