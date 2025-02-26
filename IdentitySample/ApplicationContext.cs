using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentitySample.Models;

namespace IdentitySample;


public class ApplicationContext:IdentityDbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext>options):base(options)
    {
        
    }

public DbSet<IdentitySample.Models.product> product { get; set; } = default!;


}