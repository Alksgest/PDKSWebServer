using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PDKSWebServer.Models;

namespace PDKSWebServer.DbContexts
{
    public class MainContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }

        private readonly string _connectionString;
        public MainContext() : base()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            _connectionString = configuration.GetConnectionString("SQLConnection").ToString();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
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
