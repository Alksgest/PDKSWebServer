using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PDKSWebServer.Models;

namespace PDKSWebServer.DbContexts
{
    public class CategoryContext : DbContext
    {
        private readonly string _connectionString;
        public CategoryContext() : base()
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
        public DbSet<Category> Categories { get; set; }
    }
}
