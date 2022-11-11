namespace Skipper.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer("Server=.\\SQLEXPRESS;Database=userdb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<User> Users => Set<User>();
    }
}
