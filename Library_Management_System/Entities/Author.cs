using Library_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Library_Management_System.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string FullName => $"{FName} {LName}";
        public ICollection<Book> Books { get; set; } = new List<Book>();

        public void Add_Author(string Fname,string Lname)
        {
            using (var context = new AppDbContext())
            {
                context.Authors.Add(new Author
                {
                    FName = Fname,
                    LName = Lname
                });

                context.SaveChanges();
            }
        }

        public static void Add_Author(string Fname, string Lname,IEnumerable<Book> books)
        {
            using (var context = new AppDbContext())
            {
                context.Authors.Add(new Author
                {
                    FName = Fname,
                    LName = Lname,
                    Books = books.ToList()
                });

                context.SaveChanges();
            }
        }

        public static void Get_Author()
        {
            using (var context = new AppDbContext())
            {
                var authors = context.Authors
                    .Include(x => x.Books)
                    .ToList();
                Console.WriteLine("\n----Authors----\n");
                foreach (var item in authors)
                {
                    Console.WriteLine($"\nID : {item.Id,-3}, Name : {item.FullName,-17} ");

                    Console.WriteLine("\n\t\t┌---------┬------------┬-------------┐");
                    Console.WriteLine($"\t\t│ Book Id │ Book title │ Is borrowed │");
                    Console.WriteLine("\t\t│---------│------------│-------------│");
                    foreach (var book in item.Books)
                    {
                        Console.WriteLine($"\t\t│  {book.Id,-7}│   {book.Title,-8} │    {(book.IsBorrowed ? "Yes" : "No"),-8} │");
                        Console.WriteLine("\t\t│---------│------------│-------------│");
                    }
                }
            }
        }

        public static void Get_Author(int id)
        {
            using (var context = new AppDbContext())
            {
                if (context.Authors.Any(x => x.Id == id))
                {
                    var author = context.Authors
                        .Include(x => x.Books)
                        .FirstOrDefault(x => x.Id == id);

                    Console.WriteLine($"\nID : {author.Id,-3}, Name : {author.FullName,-17} ");

                    Console.WriteLine("\n\t\t┌---------┬------------┬-------------┐");
                    Console.WriteLine($"\t\t│ Book Id │ Book title │ Is borrowed │");
                    Console.WriteLine("\t\t│---------│------------│-------------│");
                    foreach (var book in author.Books)
                    {
                        Console.WriteLine($"\t\t│  {book.Id,-7}│   {book.Title,-8} │    {(book.IsBorrowed ? "Yes" : "No"),-8} │");
                        Console.WriteLine("\t\t│---------│------------│-------------│");
                    }
                }
                else
                    Console.WriteLine($"\n\nthere is no author have id {id}");

            }
        }

        public void Update_Author(int AuthorId,string Fname,string Lname)
        {
            using (var context = new AppDbContext())
            {
                var author = context.Authors.FirstOrDefault(x => x.Id == AuthorId);

                if (author.FName != Fname && !Fname.IsNullOrEmpty())
                {
                    author.FName = Fname;
                }

                if (author.LName != Lname && !Lname.IsNullOrEmpty())
                {
                    author.LName = Lname;
                }

                context.SaveChanges();
            }
        }

        public static void Update_Author_Books(int AuthorId , int BookId)
        {
            using (var context = new AppDbContext())
            {
                var book = context.Books.FirstOrDefault(x => x.Id == BookId);

                Console.Write("Enter new title : ");
                string title = Console.ReadLine();

                if (book.Title != title && !title.IsNullOrEmpty())
                {
                    book.Title = title;
                }else
                    Console.WriteLine("There is an error !");

                context.SaveChanges();
            }
        }

        public void Delete_Author(int id,char sure)
        {
            using (var context = new AppDbContext())
            {
                if (context.Authors.Any(x => x.Id == id))
                {
                    if (sure == 'Y')
                    {
                        var author = context.Authors.FirstOrDefault(x => x.Id == id);
                        context.Authors.Remove(author);
                        context.SaveChanges();
                    }
                    else
                        Console.WriteLine("\n\nYou are welcome");
                }
                else
                    throw new Exception($"\n\nthere is no author have id {id}");
            }
        }
    }
}
