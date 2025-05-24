using Domain.Entities.Credential;
using Domain.Entities.Jobs;
using Domain.Entities.User.Candidate;
using Domain.Entities.User.Company;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Vaggi.Infra.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

        bool pendingMigrationsFound = Database.GetPendingMigrations().Any();
        if (pendingMigrationsFound)
        {
            Database.Migrate();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<AccountCredential> AccountCredential { get; set; }
    public DbSet<Candidate> Candidate { get; set; }
    public DbSet<Company> Company { get; set; }
    public DbSet<Vacancy> Vacancy { get; set; }
    public DbSet<VacancyApplication> Application { get; set; }
    public DbSet<Interview> Interview { get; set; }
    public DbSet<Message> Message { get; set; }
}
