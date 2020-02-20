using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PdksPersistence.Models;

namespace PdksPersistence.DbContexts
{
    public class MainContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<AuthorizedUser> AuthorizedUsers { get; set; }

        private readonly string _connectionString;
            
        public MainContext() : base()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            _connectionString = configuration.GetConnectionString("SQLConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString, b => b.MigrationsAssembly("PDKSWebServer"));
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<ArticleCategory>()
        //    //    .HasKey(bc => new { bc.ArticleId, bc.CategoryId });
        //    //modelBuilder.Entity<ArticleCategory>()
        //    //    .HasOne(bc => bc.Article)
        //    //    .WithMany(b => b.ArticleCategories)
        //    //    .HasForeignKey(bc => bc.ArticleId);
        //    //modelBuilder.Entity<ArticleCategory>()
        //    //    .HasOne(bc => bc.Category)
        //    //    .WithMany(c => c.Categories)
        //    //    .HasForeignKey(bc => bc.CategoryId);
        //}

        //public DbSet<ArticleCategory> ArticleCategory { get; set; }
    }
}
