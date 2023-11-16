using Forum.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Forum.Context;
public class AppDbContext : IdentityDbContext<User> {
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
  public DbSet<Community> Communities { get; set; }
  public DbSet<Post> Posts { get; set; }

  protected override void OnModelCreating(ModelBuilder builder) {
    base.OnModelCreating(builder);

    builder.Entity<Community>().HasMany(c => c.Posts);
    builder.Entity<Community>().HasMany(c => c.Members).WithMany("CommunitiesMember").UsingEntity(e => e.ToTable("CommunityUserMembers"));
    builder.Entity<Community>().HasMany(c => c.Mods).WithMany("CommunitiesMod").UsingEntity(e => e.ToTable("CommunityUserMods"));
  }
}
