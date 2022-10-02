using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebLabsAsp.Entities;

namespace WebLabsAsp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public new DbSet<ApplicationUser> Users { get; set; }
    public new DbSet<Car> Cars { get; set; }
    public new DbSet<CarGroup> CarGroups { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");
        base.OnModelCreating(modelBuilder);
    }
}