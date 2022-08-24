using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>()
                .HasData(
                    new Skill { Id = 1, Name = "FireBall", Damage = 30 },
                    new Skill { Id = 2, Name = "ShieldBash", Damage = 25 },
                    new Skill { Id = 3, Name = "Poke", Damage = 3 }
                );                
        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }

    }
}