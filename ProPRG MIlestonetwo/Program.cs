using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProPRG_MIlestonetwo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Book> books = new List<Book>();
            List<Member> members = new List<Member>();
            //List<Loan> loans = new List<Loan>();


            int userChoice = 0;
            do
            {
                DisplayMenu();
                Console.WriteLine("Enter your choice (1-8): ");


                if (!int.TryParse(Console.ReadLine(), out userChoice))
                {
                    Console.WriteLine("Error: Please enter a valid number.");
                    continue;
                }

                switch (userChoice)
                {
                    case 1:
                        AddBook(books);
                        break;
                    case 2:
                        RegisterMember(members);
                        break;
                    case 3:
                        DisplayBooks(books);
                        break;
                    case 4:
                        DisplayMembers(members);
                        break;
                    //case 5:
                    //    BorrowBook(books, members, loans);
                    //    break;
                    //case 6:
                    //    ReturnBook(books, loans);
                    //    break;
                    //case 7:
                    //    DisplayOverdueBooks(loans);
                    //    break;
                    case 8:
                        Console.WriteLine("");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select an option between 1 and 8.");
                        break;
                }



            } while (userChoice != 8);
        }




        static void DisplayMenu()
        {
            Console.WriteLine("\n===================================================");
            Console.WriteLine("         Community Library System     ");
            Console.WriteLine("=====================================================");
            Console.WriteLine("1. Add New Book");
            Console.WriteLine("2. Register New Member");
            Console.WriteLine("3. View All Books");
            Console.WriteLine("4. View All Members");
            Console.WriteLine("5. Borrow a Book");
            Console.WriteLine("6. Return a Book");
            Console.WriteLine("7. View Overdue Books");
            Console.WriteLine("8. Exit system");
            Console.WriteLine("======================================================");
        }

        class Book
        {
            public string BookID { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public bool IsAvailable { get; set; }

            public Book(string bookID, string title, string author, bool isAvailable)
            {
                BookID = bookID;
                Title = title;
                Author = author;
                IsAvailable = isAvailable;
            }

            public override string ToString()
            {
                return $"BookID: {BookID}, Title: {Title}, Author: {Author}, Available: {IsAvailable}";
            }
        }
        static bool BookExists(List<Book> books, string bookID)
        {
            foreach (var book in books)
            {
                if (book.BookID == bookID)
                    return true;
            }
            return false;
        }

        static void AddBook(List<Book> books)
        {
            string bookID, title, author;

            // Validate the Book ID
            do
            {
                Console.WriteLine("Enter Book ID: (e.g., B001)");
                bookID = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(bookID))
                {
                    Console.WriteLine("Error: Book ID cannot be empty.");
                    continue;
                }

                if (BookExists(books, bookID))
                {
                    Console.WriteLine("Error: A book with this ID already exists.");
                    bookID = ""; // forces repeat
                }
            } while (string.IsNullOrWhiteSpace(bookID) || BookExists(books, bookID));

            // Validate the Tittle
            do
            {
                Console.WriteLine("Enter Book Title: ");
                title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Error: Book title cannot be empty.");
                }
            } while (string.IsNullOrWhiteSpace(title));

            // Validate the Author
            do
            {
                Console.WriteLine("Enter Book Author: ");
                author = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(author))
                {
                    Console.WriteLine("Error: Book author cannot be empty.");
                }
            } while (string.IsNullOrWhiteSpace(author));

            // Create and add the book to the list
            Book newBook = new Book(bookID, title, author, true);
            books.Add(newBook);
            Console.WriteLine("Book added successfully.");
        }

        static bool MemberExists(List<Member> members, string memeberID)
        {
            foreach (var member in members)
            {
                if (member.MemberID == memeberID)
                    return true;
            }
            return false;
        }

        static void RegisterMember(List<Member> members)
        {
            string memberID, name, contactNumber;

            Console.WriteLine("\n--- Register New Member ----");

            // Validate the Member ID\
            do
            {
                Console.WriteLine("Enter Member ID: (e.g., M001)");
                memberID = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(memberID))
                {
                    Console.WriteLine("Error: Member ID cannot be empty.");
                    continue;
                }
                if (MemberExists(members, memberID))
                {
                    Console.WriteLine("Error: A member with this ID already exists.");
                    memberID = ""; // forces repeat
                }
            } while (string.IsNullOrWhiteSpace(memberID) || MemberExists(members, memberID));

            // Validate the Name of the Member

            do
            {
                Console.WriteLine("Enter Member Full Name:");
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Error:  Name cannot be empty.");

                }
            } while (string.IsNullOrWhiteSpace(name));

            // Validate the Contact Number of the Member
            do
            {
                Console.WriteLine("Enter Member Contact Number:");
                contactNumber = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(contactNumber))
                {
                    Console.WriteLine("Error:  Contact Number cannot be empty.");

                }
            } while (string.IsNullOrWhiteSpace(contactNumber));

            // Create and add the member to the list
            Member newMember = new Member(memberID, name, contactNumber);
            members.Add(newMember);
            Console.WriteLine("Member registered successfully.");
        }

        class Member
        {
            public string MemberID { get; set; }
            public string Name { get; set; }
            public string ContactNumber { get; set; }
            public Member(string memberID, string name, string contactNumber)
            {
                MemberID = memberID;
                Name = name;
                ContactNumber = contactNumber;
            }
            public override string ToString()
            {
                return $"ID: {MemberID}, Name: {Name}, Contact Number: {ContactNumber}";
            }

        }

        static void DisplayBooks(List<Book> books)
        {
            Console.WriteLine("\n--- Book List ---");
            if (books.Count == 0)
            {
                Console.WriteLine("No books available.");
                return;
            }
            foreach (var book in books)
            {
                Console.WriteLine(book.ToString()); // uses Book.ToString() 
            }
        }

        static void DisplayMembers(List<Member> members)
        {
            Console.WriteLine("\n--- Member List ---");
            if (members.Count == 0)
            {
                Console.WriteLine("No members registered.");
                return;
            }
            foreach (var member in members)
            {
                Console.WriteLine(member.ToString()); // uses Member.ToString() 
            }

        }
    }
}
