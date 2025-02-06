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
    public class AuthorImplementation : IAuthorService
    {
        private readonly AppDbContext _context;

        public AuthorImplementation(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthor(string firstName, string lastName)
        {
            var author = new Author { FName = firstName, LName = lastName };
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public void UpdateAuthor(int id, string firstName, string lastName)
        {
            var author = _context.Authors.Find(id);
            if (author != null)
            {
                author.FName = firstName;
                author.LName = lastName;
                _context.SaveChanges();
            }
        }

        public void DeleteAuthor(int id)
        {
            var author = _context.Authors.Find(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
        }

        public Author GetAuthor(int id)
        {
            return _context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
        }

        public void GetAllAuthors()
        {
             Author.Get_Author();
        }
    }
}
