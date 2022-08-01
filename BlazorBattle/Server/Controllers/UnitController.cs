using BlazorBattle.Server.Data;
using BlazorBattle.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattle.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly DataContext _context;

        public UnitController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUnits()
        {
            var units = await _context.Units.ToListAsync();
            return Ok(units);
        }

        [HttpPost]
        public async Task<IActionResult> AddUnits(Unit unit)
        {
            _context.Units.Add(unit);
            await _context.SaveChangesAsync();
            return Ok(await _context.Units.ToListAsync());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnits(int id, Unit unit)
        {
            var dbUnit = await _context.Units.SingleOrDefaultAsync(u => u.UnitId == id);
            if (dbUnit == null)
            {
                return NotFound("Unit with the given Id dosn't exist!");
            }
            else
            {
                dbUnit.Attack=unit.Attack;
                dbUnit.Defense = unit.Defense;
                dbUnit.Title = unit.Title;
                dbUnit.BananaCost = unit.BananaCost;
                dbUnit.HitPoints = unit.HitPoints;

                await _context.SaveChangesAsync();
                return Ok(await _context.Units.ToListAsync());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnits(int id, Unit unit)
        {
            var dbUnit = await _context.Units.SingleOrDefaultAsync(u => u.UnitId == id);
            if (dbUnit == null)
            {
                return NotFound("Unit with the given Id dosn't exist!");
            }
            else
            {
                _context.Units.Remove(dbUnit);
                await _context.SaveChangesAsync();
                return Ok(await _context.Units.ToListAsync());
            }
        }
    }
}
