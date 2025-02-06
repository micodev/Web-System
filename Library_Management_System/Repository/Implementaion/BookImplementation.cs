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
    public class BookImplementation : IBookService
    {
        private readonly AppDbContext _context;

        public BookImplementation(AppDbContext context)
        {
            _context = context;
        }

        public void AddBook(int authorId, string title)
        {
            var author = _context.Authors.Find(authorId);
            if (author != null)
            {
                var book = new Book { Title = title, AuthorId = authorId };
                _context.Books.Add(book);
                _context.SaveChanges();
            }
        }

        public void UpdateBook(int bookId, string title, int? authorId = null)
        {
            var book = _context.Books.Find(bookId);
            if (book != null)
            {
                book.Title = title;
                if (authorId.HasValue)
                {
                    book.AuthorId = authorId.Value;
                }
                _context.SaveChanges();
            }
        }

        public void DeleteBook(int bookId)
        {
            var book = _context.Books.Find(bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public Book GetBook(int bookId)
        {
           
            return _context.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == bookId)!;
        }

        public void GetAllBooks()
        {
           Book.View_Books();
        }
    }
}
