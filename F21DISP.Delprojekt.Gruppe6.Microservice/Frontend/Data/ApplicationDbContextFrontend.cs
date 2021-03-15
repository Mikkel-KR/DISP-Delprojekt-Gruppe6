using Frontend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Data
{
    public class ApplicationDbContextFrontend : DbContext
    {
        public ApplicationDbContextFrontend() { }

        public ApplicationDbContextFrontend([NotNullAttribute] DbContextOptions options) : base(options)
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

        public DbSet<Frontend.Models.Haandvaerker> Haandvaerker { get; set; }
    }
}
