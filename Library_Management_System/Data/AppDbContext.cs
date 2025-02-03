using Library_Management_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Library_Management_System.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }  
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<BookBorrowedJoinTable> BookBorrowedJoinTables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var constr = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connection = constr.GetSection("constr").Value;

            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
