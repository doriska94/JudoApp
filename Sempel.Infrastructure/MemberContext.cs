using Microsoft.EntityFrameworkCore;
using Stempel.Domain.Model;

namespace Stempel.Infrastructure;
public class MemberContext : DbContext
{
    public DbSet<Member> Members { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
    }
}
