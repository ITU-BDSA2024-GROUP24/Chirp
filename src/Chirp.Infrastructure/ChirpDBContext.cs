using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Chirp.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Chirp.Infrastructure;


public class ChirpDBContext : IdentityDbContext<Author>
{
    public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options) { }
    
    public DbSet<Cheep>  Cheeps { get; set; }   
    
    public DbSet<Core.Author> Authors { get; set; }
    
}