using CheapestG.Model.Account;
using CheapestG.Model.Logistics;
using CheapestG.Model.Model.Account;
using CheapestG.Model.Model.Logistics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Context
{
    public class CheapestGContext : DbContext
    {
        public CheapestGContext(DbContextOptions<CheapestGContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<AppUserLogin> AppUserLogins { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Truck> Trucks { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
           .HasOne(a => a.AppUserLogin)
           .WithOne(a => a.AppUser)
           .HasForeignKey<AppUser>(c => c.UserId);
        }
    }
}
