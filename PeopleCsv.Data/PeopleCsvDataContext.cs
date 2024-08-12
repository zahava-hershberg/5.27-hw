using Microsoft.EntityFrameworkCore;

namespace PeopleCsv.Data;

public class PeopleCsvDataContext : DbContext
{
    private readonly string _connectionString;

    public PeopleCsvDataContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
    public DbSet<Person> CsvPeople { get; set; }
}