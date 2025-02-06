using Library_Management_System.Data;
using Library_Management_System.Repository.Implementaion;
using Library_Management_System.Repository.Services;
using System;

namespace Library_Management_System
{
    public class Program
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;
        private readonly IBorrowerService _borrowerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
        /// <param name="authorService">The author service.</param>
        /// <param name="bookService">The book service.</param>
        /// <param name="borrowerService">The borrower service.</param>
        public Program(IAuthorService authorService, IBookService bookService, IBorrowerService borrowerService)
        {
            _authorService = authorService;
            _bookService = bookService;
            _borrowerService = borrowerService;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var context = new AppDbContext();
            var authorService = new AuthorImplementation(context); // Initialize with actual implementation
            var bookService = new BookImplementation(context); // Initialize with actual implementation
            var borrowerService = new BorrowerImplementation(context); // Initialize with actual implementation
            var program = new Program(authorService, bookService, borrowerService);
            program.Script();
        }

        /// <summary>
        /// Runs the main script for the program.
        /// </summary>
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
                Console.Write("Enter your choice (1-8): ");
                char choice = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                try
                {
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
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Displays all books.
        /// </summary>
        public void ShowBooks()
        {
            try
            {
                _bookService.GetAllBooks();
                Console.WriteLine("Books displayed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while displaying books: {ex.Message}");
            }
        }

        /// <summary>
        /// Displays all authors.
        /// </summary>
        public void ShowAuthors()
        {
            try
            {
                _authorService.GetAllAuthors();
                Console.WriteLine("Authors displayed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while displaying authors: {ex.Message}");
            }
        }

        /// <summary>
        /// Displays all borrowers.
        /// </summary>
        public void ShowBorrowers()
        {
            try
            {
                _borrowerService.GetAllBorrowers();
                Console.WriteLine("Borrowers displayed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while displaying borrowers: {ex.Message}");
            }
        }

        /// <summary>
        /// Registers a new book.
        /// </summary>
        private void RegisterNewBook()
        {
            try
            {
                string title = GetString("Enter Book Title: ");
                int authorId = GetValidInt("Enter Author ID: ");
                _bookService.AddBook(authorId, title);
                Console.WriteLine("Book added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the book: {ex.Message}");
            }
        }

        /// <summary>
        /// Links a book to a borrower.
        /// </summary>
        private void LinkBorrow()
        {
            try
            {
                int borrowerId = GetValidInt("Enter Borrower ID: ");
                int bookId = GetValidInt("Enter Book ID to Borrow: ");
                _borrowerService.AddBorrowedBooks(borrowerId, bookId);
                Console.WriteLine("Book borrowed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while borrowing the book: {ex.Message}");
            }
        }

        /// <summary>
        /// Returns a borrowed book.
        /// </summary>
        private void ReturnBorrow()
        {
            try
            {
                int borrowerId = GetValidInt("Enter Borrower ID: ");
                int bookId = GetValidInt("Enter Book ID to Return: ");
                _borrowerService.ReturnBorrowedBook(borrowerId, bookId);
                Console.WriteLine("Book returned successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while returning the book: {ex.Message}");
            }
        }

        /// <summary>
        /// Registers a new author.
        /// </summary>
        private void RegisterNewAuthor()
        {
            try
            {
                string firstName = GetString("Enter new first name: ");
                string lastName = GetString("Enter new last name: ");
                _authorService.AddAuthor(firstName, lastName);
                Console.WriteLine("Author added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the author: {ex.Message}");
            }
        }

        /// <summary>
        /// Prompts the user for a valid integer input.
        /// </summary>
        /// <param name="prompt">The prompt message.</param>
        /// <returns>The valid integer input.</returns>
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

        /// <summary>
        /// Prompts the user for a non-null string input.
        /// </summary>
        /// <param name="prompt">The prompt message.</param>
        /// <returns>The non-null string input.</returns>
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
