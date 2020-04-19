using Interview.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interview.Api.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>(entity =>
            {
                entity.Property(a => a.Name).IsRequired().HasMaxLength(15);
                entity.Property(a => a.ContactNumber).IsRequired().HasColumnType("int");
            });

        }

        public DbSet<Agent> Agents { get; set; }
    }
}
