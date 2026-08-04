using System;
using System.Collections.Generic;

enum BookStatus
    {
        Available,
        Borrowed
    }

    class Book
    {
        public string BookID;
        public string Title;
        public string Author;
        public BookStatus Status;
    }

    class Member
    {
        public string MemberID;
        public string Name;
    }

    class Loan
    {
        public string BookID;
        public string MemberID;
        public DateTime BorrowDate;
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Book> books = new List<Book>();
            List<Member> members = new List<Member>();
            List<Loan> loans = new List<Loan>();

            int userChoice;

            do
            {
                DisplayMenu();
                userChoice = GetValidNumber("Enter choice (1-12): ", 1, 12);

                switch (userChoice)
                {
                    case 1: AddBook(books); break;
                    case 2: RegisterMember(members); break;
                    case 3: DisplayBooks(books); break;
                    case 4: DisplayMembers(members); break;
                    case 5: BorrowBook(books, members, loans); break;
                    case 6: ReturnBook(books, loans); break;
                    case 7: DisplayOverdueLoans(loans); break;
                    case 8: SearchBook(books); break;
                    case 9: SearchMember(members); break;
                    case 10: SortBooks(books); break;
                    case 11: SortMembers(members); break;
                    case 12: Console.WriteLine("Exiting..."); break;
                }

            } while (userChoice != 12);
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\n===== Library Menu =====");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Register Member");
            Console.WriteLine("3. Display Books");
            Console.WriteLine("4. Display Members");
            Console.WriteLine("5. Borrow Book");
            Console.WriteLine("6. Return Book");
            Console.WriteLine("7. View Overdue Books");
            Console.WriteLine("8. Search Book");
            Console.WriteLine("9. Search Member");
            Console.WriteLine("10. Sort Books");
            Console.WriteLine("11. Sort Members");
            Console.WriteLine("12. Exit");
        }

        static int GetValidNumber(string message, int min, int max)
        {
            int value;

            while (true)
            {
                Console.Write(message);

                if (int.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
                    return value;

                Console.WriteLine("Invalid input. Try again.");
            }
        }

        static void AddBook(List<Book> books)
        {
            Book b = new Book();

            Console.Write("Enter Book ID: ");
            string id = Console.ReadLine();

            if (books.Exists(x => x.BookID == id))
            {
                Console.WriteLine("Book ID already exists.");
                return;
            }

            b.BookID = id;

            Console.Write("Enter Title: ");
            b.Title = Console.ReadLine();

            Console.Write("Enter Author: ");
            b.Author = Console.ReadLine();

            b.Status = BookStatus.Available;

            books.Add(b);
            Console.WriteLine("Book added.");
        }

        static void RegisterMember(List<Member> members)
        {
            Member m = new Member();

            Console.Write("Enter Member ID: ");
            m.MemberID = Console.ReadLine();

            Console.Write("Enter Name: ");
            m.Name = Console.ReadLine();

            members.Add(m);
            Console.WriteLine("Member added.");
        }

        static void DisplayBooks(List<Book> books)
        {
            foreach (var b in books)
            {
                Console.WriteLine($"{b.BookID} - {b.Title} - {b.Author} - {b.Status}");
            }
        }

        static void DisplayMembers(List<Member> members)
        {
            foreach (var m in members)
            {
                Console.WriteLine($"{m.MemberID} - {m.Name}");
            }
        }

        static void BorrowBook(List<Book> books, List<Member> members, List<Loan> loans)
        {
            try
            {
                Console.Write("Enter Book ID: ");
                string bookID = Console.ReadLine();

                Console.Write("Enter Member ID: ");
                string memberID = Console.ReadLine();

                Book book = books.Find(b => b.BookID == bookID);

                if (book == null || book.Status == BookStatus.Borrowed)
                {
                    Console.WriteLine("Book not available.");
                    return;
                }

                book.Status = BookStatus.Borrowed;

                loans.Add(new Loan
                {
                    BookID = bookID,
                    MemberID = memberID,
                    BorrowDate = DateTime.Now
                });

                Console.WriteLine("Book borrowed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ReturnBook(List<Book> books, List<Loan> loans)
        {
            try
            {
                Console.Write("Enter Book ID: ");
                string bookID = Console.ReadLine();

                Loan loan = loans.Find(l => l.BookID == bookID);

                if (loan == null)
                {
                    Console.WriteLine("Loan not found.");
                    return;
                }

                int daysLate = (DateTime.Now - loan.BorrowDate).Days - 7;

                if (daysLate > 0)
                {
                    int penalty = daysLate * 5;
                    Console.WriteLine($"Late return. Penalty: R{penalty}");
                }

                Book book = books.Find(b => b.BookID == bookID);
                book.Status = BookStatus.Available;

                loans.Remove(loan);

                Console.WriteLine("Book returned.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void DisplayOverdueLoans(List<Loan> loans)
        {
            foreach (var loan in loans)
            {
                int days = (DateTime.Now - loan.BorrowDate).Days;

                if (days > 7)
                {
                    Console.WriteLine($"Book {loan.BookID} overdue by {days - 7} days");
                }
            }
        }

        static void SearchBook(List<Book> books)
        {
            Console.Write("Enter Book Title: ");
            string title = Console.ReadLine();

            Book found = books.Find(b => b.Title.ToLower() == title.ToLower());

            if (found != null)
                Console.WriteLine($"{found.BookID} - {found.Title} - {found.Author} - {found.Status}");
            else
                Console.WriteLine("Book not found.");
        }

        static void SearchMember(List<Member> members)
        {
            Console.Write("Enter Member Name: ");
            string name = Console.ReadLine();

            Member found = members.Find(m => m.Name.ToLower() == name.ToLower());

            if (found != null)
                Console.WriteLine($"{found.MemberID} - {found.Name}");
            else
                Console.WriteLine("Member not found.");
        }

       static void SortBooks(List<Book> books)
{
    books.Sort((a, b) => a.Title.CompareTo(b.Title));

    Console.WriteLine("\nBooks sorted alphabetically (A-Z):");
    foreach (var book in books)
    {
        Console.WriteLine($"{book.BookID} - {book.Title} - {book.Author} - {book.Status}");
    }
}
        static void SortMembers(List<Member> members)
        {
      members.Sort((a, b) => a.Name.CompareTo(b.Name));
      Console.WriteLine("\nMembers sorted alphabetically (A-Z):");
      foreach (var member in members)
      {
    Console.WriteLine($"{member.MemberID} - {member.Name}");
      }
    }
}
