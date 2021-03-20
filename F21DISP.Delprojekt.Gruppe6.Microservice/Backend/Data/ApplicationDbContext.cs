using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Haandvaerker>()
                .HasMany(x => x.Vaerktoejskasse)
                .WithOne(x => x.EjesAfNavigation);

            mb.Entity<Vaerktoejskasse>()
                .HasMany(x => x.Vaerktoej)
                .WithOne(x => x.LiggerIvtkNavigation);

            mb.Entity<Haandvaerker>()
                .Property(x => x.HaandvaerkerId)
                .ValueGeneratedOnAdd();

            mb.Entity<Vaerktoejskasse>()
                .Property(x => x.VTKId)
                .ValueGeneratedOnAdd();

            mb.Entity<Vaerktoej>()
                .Property(x => x.VTId)
                .ValueGeneratedOnAdd();
        }

        public DbSet<Haandvaerker> Haandvaerker { get; set; }

        public DbSet<Vaerktoejskasse> Vaerktoejskasse { get; set; }

        public DbSet<Vaerktoej> Vaerktoej { get; set; }
    }
}
