using Library_Management_System.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Repository.Services
{
    public interface IBookService
    {
        void AddBook(int authorId, string title);
        void UpdateBook(int bookId, string title, int? authorId = null);
        void DeleteBook(int bookId);
        Book GetBook(int bookId);
        public void GetAllBooks();
    }

}
