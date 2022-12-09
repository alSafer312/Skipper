namespace Skipper.Data
{
    public class DataContext : DbContext
    {
        public virtual DbSet<User> Users {get; set;}
        public virtual DbSet<UserSettings> UsersSettings { get; set; }
        public virtual DbSet<CommunicationWay> CommunicationWays { get; set; }
        public virtual DbSet<NotificationSettings> NotificationSettings { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer("Server=.\\SQLEXPRESS;Database=SkipperDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(us => us.UserSettings)
                    .WithOne(u => u.User)
                    .HasForeignKey<UserSettings>(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_User_UserSettings");
            });*/

            modelBuilder.
                Entity<User>()
                .HasOne(u => u.UserSettings)
                .WithOne(us => us.User)
                .HasForeignKey<UserSettings>(us => us.UserId);

            modelBuilder.Entity<UserSettings>(entity =>
            {
                entity.HasMany(cw => cw.CommunicationWays)
                    .WithOne( us => us.UserSettings)
                    .HasForeignKey(x => x.UserSettingsId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_UserSettings_CommWay");

                /*
                entity.HasOne(u => u.User)
                    .WithOne(us => us.UserSettings)
                    .HasForeignKey<User>(x => x.UserSettingsId)
                    .OnDelete(DeleteBehavior.Cascade);
                 */

                entity.HasMany(ns => ns.NotificationSettings)
                    .WithOne(us => us.UserSettings)
                    .HasForeignKey(x => x.UserSettingsId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_UserSettings_NotificationSettings");
            });
        }

    }
}
