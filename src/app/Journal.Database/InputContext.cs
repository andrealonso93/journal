using Journal.Domain;
using Microsoft.EntityFrameworkCore;

namespace Journal.Database
{
    public class InputContext : DbContext
    {
        public InputContext(DbContextOptions<InputContext> options) : base(options) { }

        public DbSet<Input> Inputs => Set<Input>();
    }
}
