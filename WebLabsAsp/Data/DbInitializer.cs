using Microsoft.AspNetCore.Identity;
using WebLabsAsp.Entities;

namespace WebLabsAsp.Data;


public interface IDbInitializer
{
    void Initialize();
}

public class DbInitializer : IDbInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    
    public DbInitializer(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async void Initialize()
    {
        await _context.Database.EnsureCreatedAsync();

        if (!_context.CarGroups.Any())
        {
            _context.CarGroups.Add(new CarGroup() { CarGroupId = Guid.NewGuid(), GroupName = "Sedan" });
            _context.CarGroups.Add(new CarGroup() { CarGroupId = Guid.NewGuid(), GroupName = "Hatchback" });
            _context.CarGroups.Add(new CarGroup() { CarGroupId = Guid.NewGuid(), GroupName = "SUV" });
            await _context.SaveChangesAsync();
        }
        
        // if (!_context.Cars.Any())
        // {
        //     _context.Cars.Add(new Car() { Brand = "Dodge", CarGroupId = Guid.Parse("1F7DB356-B3AE-48C5-BB5A-E8CF84782481"), Description = "Challenger", Image = "asd", Price = 100000});
        //     await _context.SaveChangesAsync();
        // }
        
        if (_context.Users.Any())
            return;

        await _roleManager.CreateAsync(new IdentityRole("admin"));
        await _roleManager.CreateAsync(new IdentityRole("user"));

        string password = "password";
        ApplicationUser admin = new ApplicationUser {
            UserName = "Admin",
            Email = "admin@gmail.com",
            EmailConfirmed = true               
        };
        
        ApplicationUser user = new ApplicationUser {
            UserName = "User",
            Email = "user@gmail.com",
            EmailConfirmed = true               
        };
        
        var result = await _userManager.CreateAsync(admin, password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(admin, "admin");
        }

        result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "user");
        }

        await _context.SaveChangesAsync();
    }
}