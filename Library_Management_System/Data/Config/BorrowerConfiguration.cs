using Library_Management_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Library_Management_System.Data.Config
{
    public class BorrowerConfiguration : IEntityTypeConfiguration<Borrower>
    {
        public void Configure(EntityTypeBuilder<Borrower> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.LName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(20)
                .IsRequired();

            builder.ToTable("Borrowers");
        }
    }
}
