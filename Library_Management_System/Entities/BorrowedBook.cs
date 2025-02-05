using Library_Management_System.Data;
using Microsoft.EntityFrameworkCore;
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
            using (var context = new AppDbContext()) 
            {
                //Book item ;
                foreach (var book in books)
                {
                    //item = context.Books.FirstOrDefault(x => x.Title == book);
                    book.Borrow();
                }
                context.SaveChanges();
            }
        }

        public static void View_Borrowed_Books()
        {
            using (var context = new AppDbContext())
            {
                var Borrowedbooks = context.BorrowedBooks
                    .Include(x => x.Borrower)
                    .Include(x => x.Books)
                    .ThenInclude(x => x.Author)
                    .ToList();

                Console.WriteLine("\n   ---- Borrowed Books ----\n");
                Console.WriteLine("\n\t\t┌---------┬------------┬-------------┬-----------------┬---------------┐");
                Console.WriteLine($"\t\t│ Book Id │ Book title │ Borrower Id │  Borrower Name  │  Author Name  │");
                Console.WriteLine("\t\t│---------│------------│-------------│-----------------│---------------│");
                foreach (var collection in Borrowedbooks)
                {
                    foreach (var item in collection.Books)
                    {
                        if (item.IsBorrowed == true)
                        {
                            Console.WriteLine($"\t\t│  {item.Id,-7}│ {item.Title,-10} │      {collection.Borrower.Id,-7}│ {collection.Borrower.FullName,-16}│ {item.Author.FullName,-14}│");
                            Console.WriteLine("\t\t│---------│------------│-------------│-----------------│---------------│");
                        }
                    }
                }
            }
        }

    }
}
