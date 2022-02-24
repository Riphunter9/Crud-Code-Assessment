using Microsoft.EntityFrameworkCore;

namespace Coding_Assessment.Models
{
    public class CustomerDbContext :DbContext
    {
        public CustomerDbContext()
        {

        }
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options ) : base( options )    
        {

        }
        public DbSet<Customer> customers { get; set; }
    }
}
