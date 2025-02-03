using System;

namespace Library_Management_System.Entities
{
    public class BorrowedBook
    {
        public int Id { get; set; }
        public DateOnly BorrowDate { get; set; }
        public int BorrowerId { get; set; }
        public Borrower Borrower { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();

        public BorrowedBook() { }

        public BorrowedBook(IEnumerable<Book> books)
        {
            if (books == null || !books.Any())
                throw new ArgumentNullException(nameof(books), "At least one book must be provided.");

            //Books = new List<Book>(books);

            foreach (var book in books)
            {
                book.Borrow();  // تحديث حالة كل كتاب إلى "تم استعارة"
            }
        }
    }
}
