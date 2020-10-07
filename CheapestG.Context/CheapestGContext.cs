using CheapestG.Model.Account;
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
    }
}
