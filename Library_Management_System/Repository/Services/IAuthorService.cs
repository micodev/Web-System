using Library_Management_System.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Repository.Services
{
    public interface IAuthorService
    {
        void AddAuthor(string firstName, string lastName);
        void UpdateAuthor(int id, string firstName, string lastName);
        void DeleteAuthor(int id);
        Author GetAuthor(int id);
        void GetAllAuthors();
    }

}
