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
            //Seed data for DB
            List<Agent> defaultAgents = new List<Agent>();
            for(int i = 1; i < 50; i++)
            {
                defaultAgents.Add(new Agent() { AgentId = i, ContactNumber = 123453231, Name = $"john doe{i}" });
            }
            modelBuilder.Entity<Agent>(entity =>
            {
                entity.HasData(defaultAgents);
                entity.Property(a => a.Name).IsRequired().HasMaxLength(15);
                entity.Property(a => a.ContactNumber).IsRequired().HasColumnType("int");
            });

        }

        public DbSet<Agent> Agents { get; set; }
    }
}
