using Microsoft.EntityFrameworkCore;
using StackBrothers_YassineBenAli.Models;

namespace StackBrothers_YassineBenAli.Data


{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
    }
}
