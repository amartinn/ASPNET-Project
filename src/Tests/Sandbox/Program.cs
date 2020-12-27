namespace Sandbox
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using CasesNET.Data;
    using CasesNET.Data.Models;
    using CasesNET.Data.Repositories;
    using CasesNET.Web;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;


    public static class Program
    {
        public static void Main(string[] args)
        {
            var db = new AppDb();

            var categories = db.Categories.ToList();

            ;
        }
    }
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public AppDb()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>()
                 .HasNoKey();
            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasNoKey();
            modelBuilder.Entity<ApplicationUser>()
                .Ignore(x => x.Logins)
                .Ignore(x => x.Roles);
            modelBuilder.Entity<Category>()
                .Ignore(x => x.CreatedOn)
                .Ignore(x => x.ModifiedOn)
                .Ignore(x => x.DeletedOn)
                .Ignore(x => x.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=CasesNET;Trusted_Connection=True;MultipleActiveResultSets=true");
                //optionsBuilder.UseLazyLoadingProxies();
            }
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<IdentityUserLogin<string>> IdentityUserLogins { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}

