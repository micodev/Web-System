using Library_Management_System.Data;
using Library_Management_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Identity.Client;

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

            #endregion

            #region Borrower

            // Addborrower();

            // Registerborrower();

            // Borrower.Get_Borrower();

            // deleteborrower();

            // Add_BorrowedBooks();

            // returnbook();

            // Borrower.Get_Borrower();

            #endregion

            #region BorrowedBooks

            // BorrowedBook.View_Borrowed_Books();

            #endregion

            // Script();
            
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

        public static int Addnewauthor()
        {
            using (var context = new AppDbContext())
            {
                Console.Write("\n\nEnter new first : ");
                string Fname = Console.ReadLine();
                Console.Write("\n\nEnter new last name : ");
                string Lname = Console.ReadLine();

                Author.Add_Author(Fname, Lname);

                var id = context.Authors.FirstOrDefault(x => x.FName == Fname && x.LName == Lname).Id;
                return id;
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

        public static void delete_book(int authorid)
        {
            Author.Get_Author(authorid);

            Console.Write("\nEnter id of book you want to delete : ");
            var id = Int32.Parse(Console.ReadLine());

            Book.Delete_Book(id);

            Author.Get_Author(authorid);
        }
        #endregion

        #region Borrower_Methods
        public static void Addborrower()
        {
            using (var context = new AppDbContext())
            {
                var FName = "Mohamed";
                var LName = "Sakran";

                var borrowbooks = context.Books.Where(x => x.Title == "Book9" || x.Title == "Book1" || x.Title == "Book5").ToList();

                var d = DateOnly.FromDateTime(DateTime.Now);

                List<BorrowedBook> borrowedBooks = new List<BorrowedBook>
                {
                    new BorrowedBook(borrowbooks)
                    {
                       BorrowDate = d,
                       Books = borrowbooks
                    }
                };

                Borrower.Add_Borrower(FName, LName, borrowedBooks);
                context.SaveChanges();
            }
        }

        public static int Addnewborrower()
        {
            using (var context = new AppDbContext())
            {
                Console.Write("\n\nEnter new first name : ");
                string Fname = Console.ReadLine();
                Console.Write("\n\nEnter new last name : ");
                string Lname = Console.ReadLine();

                Borrower.Add_Borrower(Fname, Lname);

                var id = context.Borrowers.FirstOrDefault(x => x.FName == Fname && x.LName == Lname).Id;
                return id;
            }
        }

        public static void Registerborrower()
        {
            using (var context = new AppDbContext())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Welcome in our library ☺", 20);
                Console.ResetColor();

                Console.Write("\nPlease enter your first name : ");
                var FName = Console.ReadLine();
                Console.Write("\nPlease enter your last name : ");
                var LName = Console.ReadLine();

                Borrower.Add_Borrower(FName, LName);
            }
            Console.ReadKey();
            Console.Clear();

            Borrower.Get_Borrower();
        }

        public static void deleteborrower()
        {
            Borrower.Get_Borrower();

            Console.Write("\n\nEnter id of borrower you want to delete : ");
            var id = Int32.Parse(Console.ReadLine());

            Console.Clear();
            Borrower.Delete_Borrower(id);

            Borrower.Get_Borrower();
        }

        public static int Login()
        {
            Console.Write("\n Hello\n\n Please enter your id to can login : ");
            var id = Int32.Parse(Console.ReadLine());

            Borrower.Get_Borrower(id);

            return id;
        }

        public static void returnbook()
        {
            int borrowedid = Login();

            Console.Write("\n\n Please enter id of book you want to return : ");
            var id = Int32.Parse(Console.ReadLine());

            Borrower.Return_Borrowed_Book(borrowedid, id);
        }

        #endregion
        
        public static void Script()
        {
            Console.Write("welcome do you have an id (Y/N) : ");
            char choice1 = (char) Console.ReadKey().Key;

            switch (choice1)
            {
                case 'Y':
                    {
                        Console.Write("\n\nYou are author or borrower (A/B) : ");
                        char choice2 = (char)Console.ReadKey().Key;

                        switch (choice2)
                        {
                            case 'A':
                                {
                                    Console.Write("\n\nPlease Enter Your Id : ");
                                    int authorid = Int32.Parse(Console.ReadLine());
                                    Author.Get_Author(authorid);

                                    Console.Write("\n\nYou want to Add Books or Remove (A/R) : ");
                                    char choice3 = (char)Console.ReadKey().Key;

                                    switch (choice3)
                                    {
                                        case 'A':
                                            {
                                                Book.Add_Books(authorid);
                                                Author.Get_Author(authorid);
                                            }
                                            break;

                                        case 'R':
                                            {
                                                delete_book(authorid);
                                            }
                                            break;

                                        default:
                                            Console.WriteLine("\n\nSorry wrong choice");
                                            break;
                                    }
                                }
                                break;

                            case 'B':
                                {
                                    Console.Write("\n\nPlease Enter Your Id : ");
                                    int borrowerid = Int32.Parse(Console.ReadLine());
                                    Borrower.Get_Borrower(borrowerid);

                                    Console.Write("\n\nYou want to Borrow Books or Return (B/R) : ");
                                    char choice3 = (char)Console.ReadKey().Key;

                                    switch (choice3)
                                    {
                                        case 'B':
                                            {
                                                Book.View_Books();
                                                Console.Write("\n\nPlese enter id of book you want to borrow : ");
                                                int bookborrowid = Int32.Parse(Console.ReadLine());

                                                Borrower.Add_BorrowedBooks(borrowerid, bookborrowid);
                                                Borrower.Get_Borrower(borrowerid);
                                            }
                                            break;

                                        case 'R':
                                            {
                                                Borrower.Get_Borrower(borrowerid);
                                                Console.Write("\n\nPlese enter id of book you want to return : ");
                                                int bookreturnid = Int32.Parse(Console.ReadLine());
                                                Borrower.Return_Borrowed_Book(borrowerid,bookreturnid);

                                                Borrower.Get_Borrower(borrowerid);
                                            }
                                            break;

                                        default:
                                            Console.WriteLine("\n\nSorry wrong choice");
                                            break;
                                    }
                                }
                                break;

                            default:
                                Console.WriteLine("\n\nSorry wrong choice");
                                break;
                        }

                    }
                    break;

                case 'N':
                    {
                        Console.Write("\n\nYou want to register as Author or Borrower (A/B) : ");
                        char choice4 = (char)Console.ReadKey().Key;

                        switch (choice4)
                        {
                            case 'A':
                                {
                                    int newauthorid = Addnewauthor();

                                    Console.Write("\n\nYou want to add Book (Y/N) : ");
                                    char choice5 = (char)Console.ReadKey().Key;

                                    switch (choice5)
                                    {
                                        case 'Y':
                                            {
                                                Book.Add_Books(newauthorid);
                                                Author.Get_Author(newauthorid);
                                            }
                                            break;

                                        case 'N':
                                            break;
                                        default:
                                            Console.WriteLine("\n\nSorry wrong answer");
                                            break;
                                    }
                                }
                                break;

                            case 'B':
                                {
                                    int newborrowerid = Addnewborrower();

                                    Console.Write("\n\nYou want to Borrow Book (Y/N) : ");
                                    char choice6 = (char)Console.ReadKey().Key;

                                    switch (choice6)
                                    {
                                        case 'Y':
                                            {
                                                Book.View_Books();
                                                Console.Write("\n\nPlese enter id of book you want to borrow : ");
                                                int bookborrowid = Int32.Parse(Console.ReadLine());

                                                Borrower.Add_BorrowedBooks(newborrowerid, bookborrowid);
                                                Borrower.Get_Borrower(newborrowerid);
                                            }
                                            break;

                                        case 'N':
                                            break;
                                        default:
                                            Console.WriteLine("\n\nSorry wrong answer");
                                            break;
                                    }
                                }
                                break;
                            default:
                                Console.WriteLine("Sorry wrong Choice");
                                break;
                        }
                    }
                    break;
                default:
                    Console.WriteLine("\n\nSorry wrong Choice");
                    break;
            }
        }

    }
}