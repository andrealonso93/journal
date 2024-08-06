using Journal.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Journal.Database
{
    public class JournalContext : DbContext
    {
        public JournalContext(DbContextOptions<JournalContext> options) : base(options) { }

        public DbSet<Input> Inputs => Set<Input>();
        public DbSet<User> Users => Set<User>();
    }
}
