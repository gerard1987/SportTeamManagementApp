using SportTeamManagementApp.Data.Entities;


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.IO;
using Windows.Storage;

namespace SportTeamManagementApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Coach)
                .WithOne()
                .HasForeignKey<Team>(t => t.CoachId);


            modelBuilder.Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne()
                .HasForeignKey(p => p.teamId);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Define the path to the database file.
            string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "mydatabase.db");

            // Configure SQLite as the database provider.
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }

}
