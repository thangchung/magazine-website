namespace Cik.MagazineWeb.Repository
{
    using System.Data.Entity;

    using Cik.MagazineWeb.Mapping.Magazine;
    using Cik.MagazineWeb.Mapping.User;
    using Cik.MagazineWeb.Model.Magazine;
    using Cik.MagazineWeb.Model.User;

    public class MainDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }

        public MainDbContext(string connStringName) :
            base(connStringName)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CategoryMapping());
            modelBuilder.Configurations.Add(new ItemMapping());
            modelBuilder.Configurations.Add(new ItemContentMapping());
            modelBuilder.Configurations.Add(new UserMapping());
        }
    }
}