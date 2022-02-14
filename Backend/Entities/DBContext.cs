using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Entities
{
    public class DBContext : DbContext
    {
        public DBContext()
        {
        }
        protected readonly IConfiguration Configuration;
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source = localhost; Database = ApartmentData; integrated security = True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Apartment>().ToTable("Apartment");
            modelBuilder.Entity<Message>().ToTable("Message");
            modelBuilder.Entity<Payment>().ToTable("Payment");
            modelBuilder.Entity<APIAuthority>().ToTable("APIAuthority");
        }
        public DbSet<User> User { get; set; }
        public DbSet<Apartment> Apartment { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<APIAuthority> APIAuthority { get; set; }
    }
}
