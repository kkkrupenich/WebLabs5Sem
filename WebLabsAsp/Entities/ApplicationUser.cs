using Microsoft.AspNetCore.Identity;

namespace WebLabsAsp.Entities;

public class ApplicationUser : IdentityUser
{
    public byte[] Avatar { get; set; }
}