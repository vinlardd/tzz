using Microsoft.EntityFrameworkCore;

namespace tzz.Model
{
    internal class DatabaseContext : DbContext
    {
        public DbSet<Products> Products { get; set; }
    }
}
