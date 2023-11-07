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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Define the path to the database file.
            string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "mydatabase.db");

            // Configure SQLite as the database provider.
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }


    }

}
