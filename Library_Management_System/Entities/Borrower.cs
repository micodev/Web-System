using Library_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using System;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library_Management_System.Entities
{
    public class Borrower
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string FullName => $"{FName} {LName}";
        public ICollection<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();

        public static void Add_Borrower(string Fname, string Lname)
        {
            using (var context = new AppDbContext())
            {
                context.Borrowers.Add(new Borrower
                {
                    FName = Fname,
                    LName = Lname
                });

                context.SaveChanges();
            }
        }

        public static void Add_Borrower(string Fname, string Lname, IEnumerable<BorrowedBook> borrowedbooks)
        {
            using (var context = new AppDbContext())
            {
                context.Borrowers.Add(new Borrower
                {
                    FName = Fname,
                    LName = Lname,
                    BorrowedBooks = borrowedbooks.ToList()
                });

                context.SaveChanges();
            }
        }

        public static void Get_Borrower()
        {
            using (var context = new AppDbContext())
            {
                var borrowerss = context.Borrowers
                    .Include(x => x.BorrowedBooks)
                    .ThenInclude(x => x.Books)
                    .ToList();

                Console.WriteLine("\n----Borrowers----\n");

                foreach (var item in borrowerss)
                {
                    Console.WriteLine("\n\t\t┌------┬-----------------┬---------┬------------┬-----------┐");
                    Console.WriteLine($"\t\t│  Id  │     Borrower    │ Book Id │ Book title │ Date Time │");
                    Console.WriteLine("\t\t│------│-----------------│---------│------------│-----------│");

                    if (item.BorrowedBooks != null && item.BorrowedBooks.Any())
                    {

                        foreach (var borrowbook in item.BorrowedBooks)
                        {
                            foreach (var book in borrowbook.Books)
                            {
                                Console.WriteLine($"\t\t│ {item.Id,-4} │ {item.FullName,-15} │  {book.Id,-7}│   {book.Title,-8} │  {borrowbook.BorrowDate,-9}│");
                                Console.WriteLine("\t\t│------│-----------------│---------│------------│-----------│");
                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine($"\t\t│ {item.Id,-4} │ {item.FullName,-15} │  {"N/A",-7}│   {"N/A",-8} │  {"N/A",-9}│");
                        Console.WriteLine("\t\t│------│-----------------│---------│------------│-----------│");
                    }
                }
            }
        }

        public static void Get_Borrower(int id)
        {
            using (var context = new AppDbContext())
            {
                if (context.Borrowers.AsNoTracking().Any(x => x.Id == id))
                {
                    if (context.Borrowers.Include(x => x.BorrowedBooks).AsNoTracking().FirstOrDefault(x => x.Id == id).BorrowedBooks.Any())
                    {
                        var borrower = context.Borrowers
                            .Include(x => x.BorrowedBooks)
                            .ThenInclude(x => x.Books)
                            .FirstOrDefault(x => x.Id == id);

                        Console.WriteLine($"\n\n----Hello {borrower.FullName}----\n");

                        Console.WriteLine("\n\t\t┌------┬-----------------┬---------┬------------┬-----------┐");
                        Console.WriteLine($"\t\t│  Id  │     Borrower    │ Book Id │ Book title │ Date Time │");
                        Console.WriteLine("\t\t│------│-----------------│---------│------------│-----------│");

                        if (borrower.BorrowedBooks != null && borrower.BorrowedBooks.Any())
                        {

                            foreach (var borrowbook in borrower.BorrowedBooks)
                            {
                                foreach (var book in borrowbook.Books)
                                {
                                    Console.WriteLine($"\t\t│ {borrower.Id,-4} │ {borrower.FullName,-15} │  {book.Id,-7}│   {book.Title,-8} │  {borrowbook.BorrowDate,-9}│");
                                    Console.WriteLine("\t\t│------│-----------------│---------│------------│-----------│");
                                }

                            }
                        }
                        else
                        {
                            Console.WriteLine($"\t\t│ {borrower.Id,-4} │ {borrower.FullName,-15} │  {"N/A",-7}│   {"N/A",-8} │  {"N/A",-9}│");
                            Console.WriteLine("\t\t│------│-----------------│---------│------------│-----------│");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n    You didn't borrow any book yet");
                    }
                }
                else
                {
                    Console.WriteLine($"There is no Borrower have id {id}");
                }
            }
        }

        public static void Delete_Borrower(int id)
        {
            using (var context = new AppDbContext())
            {
                var borrower = context.Borrowers.FirstOrDefault(x => x.Id == id);
                context.Borrowers.Remove(borrower);

                context.SaveChanges();
            }
        }

        public static void Add_BorrowedBooks(int borrowerid, int bookid)
        {
            using (var context = new AppDbContext())
            {
                var books = context.Books.Where(x => x.Id == bookid).ToList();

                var d = DateOnly.FromDateTime(DateTime.Now);

                var borrowedbooks = new BorrowedBook(books) { Books = books, BorrowDate = d };

                context.Borrowers.FirstOrDefault(x => x.Id == borrowerid).BorrowedBooks.Add(borrowedbooks);

                context.SaveChanges();
            }
        }

        public static void Return_Borrowed_Book(int borrowerid, int bookid)
        {
            using (var context = new AppDbContext())
            {
                var borrower = context.Borrowers
                    .Include(x => x.BorrowedBooks)
                    .ThenInclude(x => x.Books)
                    .FirstOrDefault(x => x.Id == borrowerid);

                var book = context.Books.FirstOrDefault(x => x.Id == bookid);

                var borrowedbooks = borrower.BorrowedBooks.ToList();

                foreach (var item in borrowedbooks)
                {
                    item.Books.Remove(book);
                    book.ReturnBook();
                    if (!item.Books.Any())
                    {
                        context.BorrowedBooks.Remove(item);
                    }
                }

                context.SaveChanges();
            }
        }

    }
}
