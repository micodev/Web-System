using Library_Management_System.Data;
using Library_Management_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Library_Management_System
{
    class Program
    {
        static void Main()
        {

            #region Author
            // Addauthor()

            // viewAuthors();

            // Updateauthor();

            // UpdateauthorBooks();

            // Deleteauthor(); 
            #endregion

            #region Book
            // Book.Add_Books(); 

            // Add_Books();

            // Book.View_Books();

            // update_book();

            // delete_book();

            //while (true)
            //{

            //    Console.ReadKey();
            //    Console.Clear();
            //}

            #endregion

            #region Borrower
            Addborrower();
            #endregion

        }

        #region Author_Methods

        public static void Addauthor()
        {
            using (var context = new AppDbContext())
            {
                var books = new List<Book>()
                {
                    new Book
                    {
                        Title = "Book7"
                    },
                    new Book
                    {
                        Title = "Book8"
                    },
                    new Book
                    {
                        Title = "Book9"
                    }
                };

                Author.Add_Author("Hamza", "Mahmoud", books);

                Author.Get_Author();
            }
        }

        public static void Updateauthor()
        {
            Author.Get_Author();

            Console.Write("\n\nEnter id of Author you want to update : ");
            var id = Int32.Parse(Console.ReadLine());
            Console.WriteLine();

            Author.Get_Author(id);

            Console.Write("\n\nEnter new first name of Author : ");
            string fname = Console.ReadLine();
            Console.Write("\n\nEnter new last name of Author : ");
            string lname = Console.ReadLine();

            var author = new Author();
            author.Update_Author(id, fname, lname);

            Console.WriteLine($"\nhere is new update \n");
            Author.Get_Author(id);
        }

        public static void Updateauthor(int id)
        {
            Console.Write("\n\nEnter new first name of Author : ");
            string fname = Console.ReadLine();
            Console.Write("\nEnter new last name of Author : ");
            string lname = Console.ReadLine();

            var author = new Author();
            author.Update_Author(id, fname, lname);

            Console.WriteLine($"\nhere is new update \n");
            Author.Get_Author(id);
        }

        public static void UpdateauthorBooks()
        {
            Author.Get_Author();

            Console.Write("\n\nEnter id of Author you want to update : ");
            var id = Int32.Parse(Console.ReadLine());
            Console.WriteLine();

            Author.Get_Author(id);

            Console.Write("\nUpdate Author (A) or Books (B) , Enter your choice (A/B) : ");
            char choice = (char)Console.ReadKey().Key;

            switch (choice)
            {
                case 'A':
                    Updateauthor(id);
                    break;
                case 'B':
                    Console.Write("\n\nEnter id of Author Book you want to update : ");
                    var bookid = Int32.Parse(Console.ReadLine());
                    Author.Update_Author_Books(id, bookid);
                    Author.Get_Author(id);
                    break;
                default:
                    Console.WriteLine("Wrong choice");
                    break;
            }
        }

        public static void Deleteauthor()
        {
            Author.Get_Author();

            Console.Write("\n\nEnter id of Author you want to delete : ");
            var id = Int32.Parse(Console.ReadLine());

            using (var context = new AppDbContext())
            {
                if (!context.Authors.Any(x => x.Id == id))
                {
                    Console.WriteLine($"\n\nthere is no author have id {id}");
                    return;
                }
            }

            Console.WriteLine();
            Author.Get_Author(id);

            Console.Write("\n\nAre you sure you want to delete (Y/N) : ");
            char sure = (char)Console.ReadKey().Key;

            var author = new Author();
            author.Delete_Author(id, sure);

            Author.Get_Author();
        }

        #endregion


        #region Book_Methods
        public static void Add_Books()
        {
            Author.Get_Author();

            Console.Write("\nEnter id of author you want to add book for him : ");
            var id = Convert.ToInt32(Console.ReadLine());

            Book.Add_Books(id);

            Author.Get_Author();
        }

        public static void update_book()
        {
            Book.View_Books();

            Console.Write("\nEnter id of book you want to edit : ");
            var id = Int32.Parse(Console.ReadLine());

            Book.Update_Book(id);

            Book.View_Books();
        }

        public static void delete_book()
        {
            Book.View_Books();

            Console.Write("\nEnter id of book you want to delete : ");
            var id = Int32.Parse(Console.ReadLine());

            Book.Delete_Book(id);

            Book.View_Books();
        }
        #endregion

        #region Borrower_Methods
        public static void Addborrower()
        {
            using (var context = new AppDbContext())
            {
                var d = DateOnly.FromDateTime(DateTime.Now);
                var books = new List<Book>()
                        {
                            new Book
                            {
                                Title = "Book12"
                            },
                            new Book
                            {
                                Title = "Book11"
                            },
                            new Book
                            {
                                Title = "Book7"
                            }
                        };

                var borrowedbooks = new List<BorrowedBook>()
                {
                    new BorrowedBook(books)
                    {
                        BorrowDate = d,
                        Books = books
                    }
                };

                Borrower.Add_Borrower("Mohanad", "Nader", borrowedbooks);

                //Borrower.Get_Borrower();
            }
        }

        #endregion

    }
}