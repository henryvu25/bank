using Microsoft.EntityFrameworkCore;
 
namespace bank_proj.Models
{
    public class BankContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public BankContext(DbContextOptions<BankContext> options) : base(options) { }
	    public DbSet<User> users { get; set; }
        public DbSet<Transaction> transactions { get;set; }
    }
}