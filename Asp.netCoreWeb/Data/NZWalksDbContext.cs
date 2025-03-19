using Asp.netCoreWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Asp.netCoreWeb.Data;

public class NzWalksDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
    // ctor Shortcut

    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }
}