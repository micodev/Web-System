using Library_Management_System.Data;
using System;

namespace Library_Management_System.Entities
{
    public class Borrower
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string FullName => $"{FName} {LName}";
        public ICollection<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();

        public static void Add_Borrower(string Fname, string Lname)
        {
            using (var context = new AppDbContext())
            {
                context.Borrowers.Add(new Borrower
                {
                    FName = Fname,
                    LName = Lname
                });

                context.SaveChanges();
            }
        }

        public static void Add_Borrower(string Fname, string Lname, IEnumerable<BorrowedBook> borrowedbooks)
        {
            using (var context = new AppDbContext())
            {
                context.Borrowers.Add(new Borrower
                {
                    FName = Fname,
                    LName = Lname,
                    BorrowedBooks = borrowedbooks.ToList()
                });

                context.SaveChanges();
            }
        }

    }
}
