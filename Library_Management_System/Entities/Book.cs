using Library_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Library_Management_System.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public bool IsBorrowed { get; private set; }
        public ICollection<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();
        public void Borrow()
        {
            IsBorrowed = true;
        }

        public void ReturnBook()
        {
            IsBorrowed = false;
        }

        public static void Add_Books()
        {
            using (var context = new AppDbContext())
            {
                Console.Write("Enter Book Title : ");
                var title = Console.ReadLine();

                Console.Write("Enter first name of book's author : ");
                var fname = Console.ReadLine();
                Console.Write("Enter last name of book's author : ");
                var lname = Console.ReadLine();
                
                if (context.Authors.Any(x => x.FName == fname) && context.Authors.Any(x => x.LName == lname))
                {
                    var Auth = context.Authors
                        .Include(x => x.Books)
                        .FirstOrDefault(x => x.FName == fname);

                    if(Auth.Books.Any(x => x.Title == title))
                    {
                        Console.WriteLine("Author exist and he have same book ☺");
                    }else
                    {
                        Auth.Books.Add(new Book { Title = title });
                        context.SaveChanges();
                        Console.WriteLine($"Book with title ({title}) added successfuly to author ({fname} {lname}) ☺");
                    }
                }
                else
                {
                    context.Authors.Add(new Author { FName = fname, LName = lname, Books = new List<Book> { new Book { Title = title } } });
                    context.SaveChanges();
                    Console.WriteLine($"Book with title ({title}) added successfuly to author ({fname} {lname}) ☺");
                }
            }
        }

        public static void Add_Books(int id)
        {
            using (var context = new AppDbContext())
            {
                if (context.Authors.Any(x => x.Id == id))
                {
                    var Auth = context.Authors
                        .Include(x => x.Books)
                        .FirstOrDefault(x => x.Id == id);

                    Console.Write("\n\nEnter Book Title : ");
                    var title = Console.ReadLine();

                    if (Auth.Books.Any(x => x.Title == title))
                    {
                        Console.WriteLine("\nAuthor have same book ☺");
                    }
                    else
                    {
                        Auth.Books.Add(new Book { Title = title });
                        context.SaveChanges();
                        Console.WriteLine($"\nBook with title ({title}) added successfuly to author ({Auth.FullName}) ☺");
                    }
                }
                else
                {
                    Console.WriteLine($"\nThere is no author with id {id}");
                }
            }
        }

        public static void View_Books()
        {
            using (var context = new AppDbContext())
            {
                var books = context.Books
                    .Include(x => x.Author)
                    .ToList();

                Console.WriteLine("\n----Books----\n");
                Console.WriteLine("\n\t\t┌---------┬------------┬-------------┬-----------┬---------------┐");
                Console.WriteLine($"\t\t│ Book Id │ Book title │ Is borrowed │ Author Id │  Author Name  │");
                Console.WriteLine("\t\t│---------│------------│-------------│-----------│---------------│");
                foreach (var item in books)
                {
                    Console.WriteLine($"\t\t│  {item.Id,-7}│ {item.Title,-10} │    {(item.IsBorrowed ? "Yes" : "No"),-8} │    {item.Author.Id,-7}│ {item.Author.FullName,-14}│");
                    Console.WriteLine("\t\t│---------│------------│-------------│-----------│---------------│");

                }
            }
        }

        public static void Update_Book(int bookid)
        {
            using (var context = new AppDbContext())
            {
                if (context.Books.Any(x => x.Id == bookid))
                {
                    var book = context.Books.FirstOrDefault(x => x.Id == bookid);

                    Console.Write("\nYou want to change author (Y/N) => Y for change , N for just edit title : ");
                    char choice = (char) Console.ReadKey().Key;

                    switch (choice)
                    {
                        case 'Y':
                            Console.Write("\n\nIf author exist please enter his id if not write N : ");
                            var kchoice = Console.ReadLine();

                            if (Int32.TryParse(kchoice, out int idauthor))
                            {
                                book.Author = context.Authors.FirstOrDefault(x => x.Id == idauthor);
                                context.SaveChanges();
                                Console.WriteLine("\n\nChanged");
                                View_Books();
                            }
                            else if(kchoice.ToLower() == "n")
                            {
                                Console.Write("\nEnter first name for author : ");
                                var fname = Console.ReadLine();
                                Console.Write("\nEnter last name for author : ");
                                var lname = Console.ReadLine();

                                book.Author = new Author { FName = fname, LName = lname };
                                context.SaveChanges();
                                Console.WriteLine("\n\nBook added to new author");
                            }else
                                Console.WriteLine("\n\nUnknow choice");

                            break;
                        case 'N':
                            Console.Write("\n\nEnter new title : ");
                            var title = Console.ReadLine();
                            book.Title = title;
                            context.SaveChanges();
                            Console.WriteLine("\n\nBook's title is changed");

                            break;
                        default:
                            Console.WriteLine("\n\nUnknow choice");
                            break;
                    }
                    
                }
                else
                {
                    Console.WriteLine($"There is no book with id {bookid}");
                }
            }
        }

        public static void Delete_Book(int id)
        {
            using (var context = new AppDbContext())
            {
                if(context.Books.Any(x => x.Id == id))
                {
                    var book = context.Books.FirstOrDefault(x => x.Id == id);
                    context.Books.Remove(book);
                    context.SaveChanges();
                    Console.WriteLine($"\n\nBook with id ({id}) is removed successfully");
                }
                else
                {
                    Console.WriteLine($"\n\nThere is no book with id {id}");
                }
            }
        }

    }
}
