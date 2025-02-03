using Library_Management_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Library_Management_System.Data.Config
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.IsBorrowed)
                .HasDefaultValue(false);

            builder.HasOne(a => a.Author)
                .WithMany(b => b.Books)
                .HasForeignKey(b => b.AuthorId)
                .IsRequired();

            builder.ToTable("Books");
        }
    }
}
