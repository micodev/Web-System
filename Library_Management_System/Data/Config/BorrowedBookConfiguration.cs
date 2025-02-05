using Library_Management_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Library_Management_System.Data.Config
{
    public class BorrowedBookConfiguration : IEntityTypeConfiguration<BorrowedBook>
    {
        public void Configure(EntityTypeBuilder<BorrowedBook> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.BorrowDate)
                   .HasConversion(
                         v => v.ToDateTime(TimeOnly.MinValue), 
                         v => DateOnly.FromDateTime(v)         
                   )
                   .HasColumnType("DATE");

            builder.HasOne(x => x.Borrower)
                .WithMany(x => x.BorrowedBooks)
                .HasForeignKey(x => x.BorrowerId)
                .IsRequired();

            builder.HasMany(x => x.Books)
                .WithMany(x => x.BorrowedBooks)
                .UsingEntity<BookBorrowedJoinTable>();

            builder.ToTable("BorrowedBooks");
        }
    }
}
