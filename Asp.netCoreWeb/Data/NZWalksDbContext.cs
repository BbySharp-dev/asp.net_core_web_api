using Asp.netCoreWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Asp.netCoreWeb.Data;

public class NzWalksDbContext: DbContext
{
    // ctor Shortcut
    public NzWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
    {
        
    }

    public DbSet<Difficulty> Difficulties  { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }
}