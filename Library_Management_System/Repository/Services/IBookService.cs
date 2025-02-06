using Library_Management_System.Entities;


namespace Library_Management_System.Repository.Services
{
    /// <summary>
    /// Provides methods for managing books in the library management system.
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// Adds a new book to the system.
        /// </summary>
        /// <param name="authorId">The ID of the author of the book.</param>
        /// <param name="title">The title of the book.</param>
        /// <exception cref="ArgumentException">Thrown when the title is null or empty.</exception>
        /// <exception cref="KeyNotFoundException">Thrown when the author with the specified ID is not found.</exception>
        void AddBook(int authorId, string title);

        /// <summary>
        /// Updates the details of an existing book.
        /// </summary>
        /// <param name="bookId">The ID of the book to update.</param>
        /// <param name="title">The new title of the book.</param>
        /// <param name="authorId">The new author ID of the book (optional).</param>
        /// <exception cref="ArgumentException">Thrown when the title is null or empty.</exception>
        /// <exception cref="KeyNotFoundException">Thrown when the book or author with the specified ID is not found.</exception>
        void UpdateBook(int bookId, string title, int? authorId = null);

        /// <summary>
        /// Deletes a book from the system.
        /// </summary>
        /// <param name="bookId">The ID of the book to delete.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the book with the specified ID is not found.</exception>
        void DeleteBook(int bookId);

        /// <summary>
        /// Retrieves a book by its ID.
        /// </summary>
        /// <param name="bookId">The ID of the book to retrieve.</param>
        /// <returns>The book with the specified ID.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the book with the specified ID is not found.</exception>
        Book GetBook(int bookId);

        /// <summary>
        /// Retrieves all books in the system.
        /// </summary>
        void GetAllBooks();
    }
}

