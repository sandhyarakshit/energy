using Microsoft.EntityFrameworkCore;
using SubwayPromotion.Models;
 
namespace SubwayPromotion.Data
{
    public class LocationDbContext : DbContext
    {
        public LocationDbContext(DbContextOptions<LocationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LocationDTO> Location { get; set; }
        public DbSet<PromotionDTO> Promotions { get; set; }
        public DbSet<TermDTO> Terms { get; set; }
        // Add the DbSet for your third DTO here
    }
}