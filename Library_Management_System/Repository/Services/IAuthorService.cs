using Library_Management_System.Entities;


namespace Library_Management_System.Repository.Services
{
    /// <summary>
    /// Provides methods for managing authors in the library management system.
    /// </summary>
    public interface IAuthorService
    {
        /// <summary>
        /// Adds a new author to the system.
        /// </summary>
        /// <param name="firstName">The first name of the author.</param>
        /// <param name="lastName">The last name of the author.</param>
        /// <exception cref="ArgumentException">Thrown when the first name or last name is null or empty.</exception>
        void AddAuthor(string firstName, string lastName);

        /// <summary>
        /// Updates the details of an existing author.
        /// </summary>
        /// <param name="id">The ID of the author to update.</param>
        /// <param name="firstName">The new first name of the author.</param>
        /// <param name="lastName">The new last name of the author.</param>
        /// <exception cref="ArgumentException">Thrown when the first name or last name is null or empty.</exception>
        /// <exception cref="KeyNotFoundException">Thrown when the author with the specified ID is not found.</exception>
        void UpdateAuthor(int id, string firstName, string lastName);

        /// <summary>
        /// Deletes an author from the system.
        /// </summary>
        /// <param name="id">The ID of the author to delete.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the author with the specified ID is not found.</exception>
        void DeleteAuthor(int id);

        /// <summary>
        /// Retrieves an author by their ID.
        /// </summary>
        /// <param name="id">The ID of the author to retrieve.</param>
        /// <returns>The author with the specified ID.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the author with the specified ID is not found.</exception>
        Author GetAuthor(int id);

        /// <summary>
        /// Retrieves all authors in the system.
        /// </summary>
        void GetAllAuthors();
    }
}
