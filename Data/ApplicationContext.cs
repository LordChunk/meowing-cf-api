using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationContext : DbContext
    {
        // DbSets
        public DbSet<HttpHeader> HttpHeaders { get; set; }
        public DbSet<HttpRequest> HttpRequests { get; set; }
        public DbSet<HttpRequestLog> HttpRequestLog { get; set; }
        public DbSet<RequestUrl> RequestUrls { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set many to many relationship between HttpHeader and HttpRequest
            modelBuilder.Entity<HttpHeader>()
                .HasMany(h => h.HttpRequests)
                .WithMany(r => r.Headers);

            // Set one to many relationship between HttpRequest and RequestUrl
            modelBuilder.Entity<HttpRequest>()
                .HasOne(r => r.Url)
                .WithMany(u => u.Requests)
                .HasForeignKey(r => r.UrlId);

            // Add DateTime value to HttpRequestLog
            modelBuilder.Entity<HttpRequestLog>()
                .Property(l => l.CreatedAt)
                //.HasDefaultValueSql("GETUTCDATE()");
                .HasDefaultValueSql("CAST(NOW() at time zone 'utc' AS date)");
        }
    }
}