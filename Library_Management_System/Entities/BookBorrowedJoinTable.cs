using System;

namespace Library_Management_System.Entities
{
    public class BookBorrowedJoinTable
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int BorrowedBookId { get; set; }
        public BorrowedBook BorrowedBook { get; set; }
    }
}
