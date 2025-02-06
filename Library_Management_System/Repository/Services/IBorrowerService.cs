using Library_Management_System.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Repository.Services
{
    public interface IBorrowerService
    {
        void AddBorrower(string firstName, string lastName);
        void UpdateBorrower(int id, string firstName, string lastName);
        void DeleteBorrower(int id);
        Borrower GetBorrower(int id);
        void GetAllBorrowers();
        void AddBorrowedBooks(int borrowerId, int bookId);
        void ReturnBorrowedBook(int borrowerId, int bookId);
    }
}
