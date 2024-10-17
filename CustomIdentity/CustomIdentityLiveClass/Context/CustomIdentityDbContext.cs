using CustomIdentityLiveClass.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomIdentityLiveClass.Context
{
    public class CustomIdentityDbContext : DbContext
    {
        public CustomIdentityDbContext(DbContextOptions<CustomIdentityDbContext> options)  : base(options)
        {
            
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
    }
}
