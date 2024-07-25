using Microsoft.EntityFrameworkCore;
using Stempel.Domain.Model;

namespace Stempel.Infrastructure;
public class StampContext : DbContext
{
    private readonly string _connectionString;
    public DbSet<Member> Members { get; set; }
    public DbSet<StampTime> StampTime { get; set; }
    public StampContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
    }
}
