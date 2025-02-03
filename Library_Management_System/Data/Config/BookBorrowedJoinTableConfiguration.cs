using Library_Management_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Library_Management_System.Data.Config
{
    public class BookBorrowedJoinTableConfiguration : IEntityTypeConfiguration<BookBorrowedJoinTable>
    {
        public void Configure(EntityTypeBuilder<BookBorrowedJoinTable> builder)
        {
            builder.HasKey(x => new { x.BorrowedBookId, x.BookId });

            builder.ToTable("BookBorrowedJoinTables");
        }
    }
}
