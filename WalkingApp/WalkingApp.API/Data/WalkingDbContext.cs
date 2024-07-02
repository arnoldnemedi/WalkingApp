using Microsoft.EntityFrameworkCore;
using WalkingApp.API.Models.Domain;

namespace WalkingApp.API.Data
{
    public class WalkingWalkingDbContext: DbContext
    {
        public WalkingWalkingDbContext(DbContextOptions<WalkingWalkingDbContext> dbContextOptions): base(dbContextOptions)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var difficulties = new List<Difficulty>()
            { 
                new Difficulty
                {
                    Id = Guid.Parse("8f878990-cdf0-47ca-93f2-e868c80f3a4b"),
                    Name = "Easy"
                },
                new Difficulty
                {
                    Id = Guid.Parse("2f70e6f4-b46b-4a56-8504-73d06ae62940"),
                    Name = "Medium"
                },
                new Difficulty
                {
                    Id = Guid.Parse("7b849395-b5a3-4a38-9f44-616e5e5ddca2"),
                    Name = "Hard"
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            var regions = new List<Region>()
            { 
                new Region
                {
                   Id = Guid.Parse("63169c6e-b896-43b2-9aab-a907c1127b7f"),
                   Name = "Auckland",
                   Code = "AKL",
                   RegionImageUrl = ""
                }
            };

            modelBuilder.Entity<Region>().HasData(regions);

        }
    }
}
