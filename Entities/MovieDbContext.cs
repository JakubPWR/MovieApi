using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieApi.Entities
{
    public class MovieDbContext:DbContext
    {
        private string _connectionString = "Data Source=MovieDb.db;";
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(m=>
            {
                m.Property(m=>m.Name).IsRequired();
                m.Property(m=>m.Genre).IsRequired();
                m.HasIndex(m=>m.Name).IsUnique();
            });
            modelBuilder.Entity<Rating>(r=>
            {
                r.Property(r=>r.Score).IsRequired();
            });
            modelBuilder.Entity<User>(u=>
            {
                u.Property(u=>u.Name).IsRequired();
                u.Property(u=>u.Email).IsRequired();
                u.HasIndex(u => u.Name).IsUnique();
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}