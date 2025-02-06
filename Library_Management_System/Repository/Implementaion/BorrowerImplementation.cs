using Library_Management_System.Data;
using Library_Management_System.Entities;
using Library_Management_System.Repository.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Repository.Implementaion
{
    internal class BorrowerImplementation : IBorrowerService
    {
        private readonly AppDbContext _context;

        public BorrowerImplementation(AppDbContext context)
        {
            _context = context;
        }

        public void AddBorrower(string firstName, string lastName)
        {
            var borrower = new Borrower { FName = firstName, LName = lastName };
            _context.Borrowers.Add(borrower);
            _context.SaveChanges();
        }

        public void UpdateBorrower(int id, string firstName, string lastName)
        {
            var borrower = _context.Borrowers.Find(id);
            if (borrower != null)
            {
                borrower.FName = firstName;
                borrower.LName = lastName;
                _context.SaveChanges();
            }
        }

        public void DeleteBorrower(int id)
        {
            var borrower = _context.Borrowers.Find(id);
            if (borrower != null)
            {
                _context.Borrowers.Remove(borrower);
                _context.SaveChanges();
            }
        }

        public Borrower GetBorrower(int id)
        {
            return _context.Borrowers.Include(b => b.BorrowedBooks).ThenInclude(bb => bb.Books).FirstOrDefault(b => b.Id == id);
        }

        public void GetAllBorrowers()
        {
             Borrower.Get_Borrower();
        }
        public void AddBorrowedBooks(int borrowerId, int bookId)
        {
            var borrower = _context.Borrowers
                .Include(b => b.BorrowedBooks)
                .ThenInclude(bb => bb.Books)
                .FirstOrDefault(b => b.Id == borrowerId);

            if (borrower == null)
            {
                throw new KeyNotFoundException($"Borrower with ID {borrowerId} not found.");
            }

            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }

            if (book.IsBorrowed)
            {
                throw new InvalidOperationException($"Book with ID {bookId} is already borrowed.");
            }

            var borrowedBook = new BorrowedBook
            {
                BorrowDate = DateOnly.FromDateTime(DateTime.Now),
                BorrowerId = borrowerId,
                Books = new List<Book> { book }
            };

            borrower.BorrowedBooks.Add(borrowedBook);
            book.Borrow();

            _context.SaveChanges();
        }
        public void ReturnBorrowedBook(int borrowerId, int bookId)
        {
            var borrower = _context.Borrowers
                .Include(b => b.BorrowedBooks)
                .ThenInclude(bb => bb.Books)
                .FirstOrDefault(b => b.Id == borrowerId);

            if (borrower == null)
            {
                throw new KeyNotFoundException($"Borrower with ID {borrowerId} not found.");
            }

            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }

            var borrowedBook = borrower.BorrowedBooks.FirstOrDefault(bb => bb.Books.Contains(book));
            if (borrowedBook == null)
            {
                throw new InvalidOperationException($"Book with ID {bookId} is not borrowed by borrower with ID {borrowerId}.");
            }

            borrowedBook.Books.Remove(book);
            book.ReturnBook();

            if (!borrowedBook.Books.Any())
            {
                borrower.BorrowedBooks.Remove(borrowedBook);
                _context.BorrowedBooks.Remove(borrowedBook);
            }

            _context.SaveChanges();
        }
    }
}
