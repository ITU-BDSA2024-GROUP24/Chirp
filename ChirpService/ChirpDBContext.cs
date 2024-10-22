using Microsoft.EntityFrameworkCore;
using System;

namespace Chirp.Razor;


public class ChirpDBContext : DbContext
{
    public DbSet<Cheep> Cheeps { get; set; }   
    public DbSet<Author> Authors { get; set; }
    
    public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options) { }
}