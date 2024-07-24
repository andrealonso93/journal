using Journal.Domain;
using Microsoft.EntityFrameworkCore;

namespace Journal.Database;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<InputContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
}
