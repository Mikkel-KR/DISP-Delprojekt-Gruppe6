using System;
using System.Collections.Generic;
using System.Text;
using Backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Haandvaerker> HaandvaerkerSet { get; set; }
        public DbSet<Vaerktoejskasse> VaerktoejskasseSet { get; set; }
        public DbSet<Vaerktoej> VaerktoejSet { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Haandvaerker>()
                .HasMany<Vaerktoejskasse>()
                .WithOne(x => x.EjesAfNavigation);

            mb.Entity<Vaerktoejskasse>()
                .HasMany<Vaerktoej>()
                .WithOne(x => x.LiggerIvtkNavigation);
        }
    }
}
