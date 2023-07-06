using ComplexManagment.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComplexManagment
{
    public class DbComplexContext:DbContext
    {
        public DbComplexContext(DbContextOptions<DbComplexContext> options) : base(options)
        {

        }
        public DbSet<Complex> Complexes { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Unit> Units { get; set; }

    }
}
