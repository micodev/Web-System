using Library_Management_System.Entities;


namespace Library_Management_System.Repository.Services
{
    /// <summary>
    /// Provides methods for managing borrowers in the library management system.
    /// </summary>
    public interface IBorrowerService
    {
        /// <summary>
        /// Adds a new borrower to the system.
        /// </summary>
        /// <param name="firstName">The first name of the borrower.</param>
        /// <param name="lastName">The last name of the borrower.</param>
        /// <exception cref="ArgumentException">Thrown when the first name or last name is null or empty.</exception>
        void AddBorrower(string firstName, string lastName);

        /// <summary>
        /// Updates the details of an existing borrower.
        /// </summary>
        /// <param name="id">The ID of the borrower to update.</param>
        /// <param name="firstName">The new first name of the borrower.</param>
        /// <param name="lastName">The new last name of the borrower.</param>
        /// <exception cref="ArgumentException">Thrown when the first name or last name is null or empty.</exception>
        /// <exception cref="KeyNotFoundException">Thrown when the borrower with the specified ID is not found.</exception>
        void UpdateBorrower(int id, string firstName, string lastName);

        /// <summary>
        /// Deletes a borrower from the system.
        /// </summary>
        /// <param name="id">The ID of the borrower to delete.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the borrower with the specified ID is not found.</exception>
        void DeleteBorrower(int id);

        /// <summary>
        /// Retrieves a borrower by their ID.
        /// </summary>
        /// <param name="id">The ID of the borrower to retrieve.</param>
        /// <returns>The borrower with the specified ID.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the borrower with the specified ID is not found.</exception>
        Borrower GetBorrower(int id);

        /// <summary>
        /// Retrieves all borrowers in the system.
        /// </summary>
        void GetAllBorrowers();

        /// <summary>
        /// Links a book to a borrower.
        /// </summary>
        /// <param name="borrowerId">The ID of the borrower.</param>
        /// <param name="bookId">The ID of the book to borrow.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the borrower or book with the specified ID is not found.</exception>
        void AddBorrowedBooks(int borrowerId, int bookId);

        /// <summary>
        /// Returns a borrowed book.
        /// </summary>
        /// <param name="borrowerId">The ID of the borrower.</param>
        /// <param name="bookId">The ID of the book to return.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the borrower or book with the specified ID is not found.</exception>
        void ReturnBorrowedBook(int borrowerId, int bookId);
    }
}

