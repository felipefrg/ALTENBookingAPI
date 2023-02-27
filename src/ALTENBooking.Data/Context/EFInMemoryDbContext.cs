using ALTENBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Data.Context
{
    public class EFInMemoryDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public EFInMemoryDbContext(DbContextOptions<EFInMemoryDbContext> options) : base(options)
        {

        }      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().HasQueryFilter(p => p.Active);
            modelBuilder.Entity<Reservation>().HasQueryFilter(p => p.Active);
            modelBuilder.Entity<Room>().HasQueryFilter(p => p.Active);
        }  
    }
}
