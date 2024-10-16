namespace RenanYokoya.Models;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=AndreNadalin_RenanYokoya.db");
    }
}
