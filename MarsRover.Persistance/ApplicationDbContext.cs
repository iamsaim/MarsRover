using MarsRover.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarsRover.Persistance
{

    public class ApplicationDbContext : DbContext
    {
        public DbSet<PlateauEntity> plateaus { get; set; }
        public DbSet<RoverEntity> rovers { get; set; }
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}