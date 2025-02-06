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

        public static void Add_Author(string Fname,string Lname)
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
                var authors = context.Authors.ToList();
                Console.WriteLine("\n----Authors----\n");
                Console.WriteLine("\n\t\t┌-----------┬------------┬------------┐");
                Console.WriteLine($"\t\t│ Author Id │ First Name │ Last Name  │");
                Console.WriteLine("\t\t│-----------│------------│------------│");
                foreach (var item in authors)
                {
                    Console.WriteLine($"\t\t│ {item.Id,-9} │  {item.FName,-10}│ {item.LName,-10} │");
                    Console.WriteLine("\t\t│-----------│------------│------------│");
                }
            }
        }

        public static void Get_Author(int id)
        {
            using (var context = new AppDbContext())
            {
                if (context.Authors.Any(x => x.Id == id))
                {
                    var author = context.Authors.FirstOrDefault(x => x.Id == id);
                    Console.WriteLine("\n----Author----\n");
                    Console.WriteLine("\n\t\t┌---------┬------------┬------------┐");
                    Console.WriteLine($"\t\t│ Author Id │ First Name │ Last Name │");
                    Console.WriteLine("\t\t│-----------│------------│------------│");
                    Console.WriteLine($"\t\t│ {author.Id,-9}│  {author.FName,-10} │   {author.LName,-10} │");
                    Console.WriteLine("\t\t│-----------│------------│------------│");
                }
                else
                {
                    Console.WriteLine($"\n\nThere is no author with ID {id}");
                }
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
