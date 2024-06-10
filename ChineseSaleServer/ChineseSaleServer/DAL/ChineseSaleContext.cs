using ChineseSaleServer.Models;


namespace ChineseSaleServer.Dal
{
    public class ChineseSaleContext : DbContext
    {
        public ChineseSaleContext(DbContextOptions<ChineseSaleContext>options):base(options) 
        {
                
        }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Draft> Drafts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<RandomClass> Random { get; set; }

    }
}
