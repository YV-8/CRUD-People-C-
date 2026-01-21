using Microsoft.EntityFrameworkCore;
using PersonaApp.Models;

namespace PersonaApp.Data;

public class AppDBContext:DbContext
{
    public DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "server=localhost;database=persona;user=root;password=root123";
        optionsBuilder.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
        
    }
}