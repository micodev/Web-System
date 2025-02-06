using Library_Management_System.Data;
using Library_Management_System.Entities;
using Library_Management_System.Repository.Implementaion;
using Library_Management_System.Repository.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Identity.Client;

namespace Library_Management_System
{
    public class Program
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;
        private readonly IBorrowerService _borrowerService;

        public Program(IAuthorService authorService, IBookService bookService, IBorrowerService borrowerService)
        {
            _authorService = authorService;
            _bookService = bookService;
            _borrowerService = borrowerService;
        }

        static void Main()
        {
            var context = new AppDbContext();
            var authorService = new AuthorImplementation(context); // Initialize with actual implementation
            var bookService = new BookImplementation(context); // Initialize with actual implementation
            var borrowerService = new BorrowerImplementation(context); // Initialize with actual implementation
            var program = new Program(authorService, bookService, borrowerService);
            program.Script();
        }

        private void Script()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Add Author");
                Console.WriteLine("2. Add Book");
                Console.WriteLine("3. Link Borrow");
                Console.WriteLine("4. Return Borrow");
                Console.WriteLine("5. Show Books");
                Console.WriteLine("6. Show Authors");
                Console.WriteLine("7. Show Borrowers");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice (1-5): ");
                char choice = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        RegisterNewAuthor();
                        break;
                    case '2':
                        RegisterNewBook();
                        break;
                    case '3':
                        LinkBorrow();
                        break;
                    case '4':
                        ReturnBorrow();
                        break;
                    case '5':
                       ShowBooks();
                        break;
                    case '6':
                        ShowAuthors();
                        break;
                    case '7':
                        ShowBorrowers();
                        break;
                    case '8':
                        exit = true;
                        Console.WriteLine("Exiting the program.");
                        break;
                    default:
                        Console.WriteLine("Sorry, invalid choice.");
                        break;
                }
            }
        }
        public void ShowBooks()
        {
            _bookService.GetAllBooks();
            Console.WriteLine("Books displayed successfully.");
        }
        public void ShowAuthors()
        {
            _authorService.GetAllAuthors();
            Console.WriteLine("Authors displayed successfully.");
        }
        public void ShowBorrowers()
        {
            _borrowerService.GetAllBorrowers();
            Console.WriteLine("Borrowers displayed successfully.");
        }
        private void RegisterNewBook()
        {
            string title = GetString("Enter Book Title: ");
            int authorId = GetValidInt("Enter Author ID: ");
            _bookService.AddBook(authorId, title);
            Console.WriteLine("Book added successfully.");
        }

        private void LinkBorrow()
        {
            int borrowerId = GetValidInt("Enter Borrower ID: ");
            int bookId = GetValidInt("Enter Book ID to Borrow: ");
            _borrowerService.AddBorrowedBooks(borrowerId, bookId);
            Console.WriteLine("Book borrowed successfully.");
        }

        private void ReturnBorrow()
        {
            int borrowerId = GetValidInt("Enter Borrower ID: ");
            int bookId = GetValidInt("Enter Book ID to Return: ");
            _borrowerService.ReturnBorrowedBook(borrowerId, bookId);
            Console.WriteLine("Book returned successfully.");
        }
        private void HandleAuthor()
        {
            int authorId = GetValidInt("\n\nPlease Enter Your ID: ");
            var author = _authorService.GetAuthor(authorId);
            if (author == null)
            {
                Console.WriteLine("\nInvalid ID.");
                return;
            }

            Console.Write("\n\nDo you want to Add Books or Remove (A/R): ");
            char choice3 = char.ToUpper(Console.ReadKey().KeyChar);

            switch (choice3)
            {
                case 'A':
                    string title = GetString("\n\nEnter Book Title: ");
                    _bookService.AddBook(authorId, title);
                    break;
                case 'R':
                    int bookId = GetValidInt("\n\nEnter the ID of the book you want to delete: ");
                    _bookService.DeleteBook(bookId);
                    break;
                default:
                    Console.WriteLine("\nSorry, wrong choice.");
                    break;
            }
        }

        private void HandleBorrower()
        {
            int borrowerId = GetValidInt("\n\nPlease Enter Your ID: ");
            var borrower = _borrowerService.GetBorrower(borrowerId);
            if (borrower == null)
            {
                Console.WriteLine("\nInvalid ID.");
                return;
            }

            Console.Write("\n\nDo you want to Borrow Books or Return (B/R): ");
            char choice3 = char.ToUpper(Console.ReadKey().KeyChar);

            switch (choice3)
            {
                case 'B':
                    int bookId = GetValidInt("\n\nPlease enter the ID of the book you want to borrow: ");
                    _borrowerService.AddBorrowedBooks(borrowerId, bookId);
                    break;
                case 'R':
                    bookId = GetValidInt("\n\nPlease enter the ID of the book you want to return: ");
                    _borrowerService.ReturnBorrowedBook(borrowerId, bookId);
                    break;
                default:
                    Console.WriteLine("\nSorry, wrong choice.");
                    break;
            }
        }

        private void RegisterNewAuthor()
        {
            string firstName = GetString("\n\nEnter new first name: ");
            string lastName = GetString("\n\nEnter new last name: ");
            _authorService.AddAuthor(firstName, lastName);
        }

        private void RegisterNewBorrower()
        {
            string firstName = GetString("\n\nEnter new first name: ");
            string lastName = GetString("\n\nEnter new last name: ");
            _borrowerService.AddBorrower(firstName, lastName);
        }

        private int GetValidInt(string prompt)
        {
            int result;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.Write("Invalid input. " + prompt);
            }
            return result;
        }

        private string GetString(string prompt)
        {
            string? input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (input == null)
                {
                    Console.WriteLine("Input cannot be null.");
                }
            } while (input == null);
            return input;
        }
    }
}