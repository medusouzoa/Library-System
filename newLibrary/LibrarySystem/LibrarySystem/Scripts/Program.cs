using System;

namespace LibrarySystem.Scripts
{
  internal class Program
  {
    public static void Main(string[] args)
    {
      LibraryClass library = new LibraryClass();
      library.InsertBooks();
      library.InsertBorrowedBooks();

      while (true)
      {
        Console.WriteLine("1. Add Book");
        Console.WriteLine("2. List the books");
        Console.WriteLine("3. Search a book");
        Console.WriteLine("4. Borrow a book");
        Console.WriteLine("5. Return a book");
        Console.WriteLine("6. Show Expired dated books");
        Console.WriteLine("7. List the borrowed books");
        Console.WriteLine("0. Exit");

        Console.Write("Choose an option (Simply, type the number you want to perform): ");
        string choice = Console.ReadLine();

        switch (choice)
        {
          case "1":
            AddNewBook(library);
            break;
          case "2":
            library.ListTheBooks();
            break;
          case "3":
            SearchBook(library);
            break;
          case "4":
            BorrowBook(library);
            break;
          case "5":
            ReturnTheBook(library);
            break;
          case "6":
            ShowExpiredDate(library);
            break;
          case "7":
            library.ListTheBorrowedBooks();
            break;
          case "0":
            library.SaveTheBooks();
            Environment.Exit(0);
            break;
          default:
            Console.WriteLine("Invalid choice. Please try again. ");
            break;
        }
      }
    }

    static void AddNewBook(LibraryClass library)
    {
      Console.Write("Title of the book: ");
      string title = Console.ReadLine();
      Console.Write("Writer of the book: ");
      string writer = Console.ReadLine();
      Console.Write("ISBN: ");
      string isbn = Console.ReadLine();
      Console.Write("Copy count of the book: ");
      int copyCount = Convert.ToInt32(Console.ReadLine());

      BookClass newBook = new BookClass
      {
        title = title,
        writer = writer,
        ISBN = isbn,
        copyCount = copyCount
      };

      if (library.ExactBookSearch(isbn) != null)
      {
        library.ExactBookSearch(isbn).copyCount++;
        Console.WriteLine("Book already exists. Book count increased.");
      }
      else
      {
        library.AddBook(newBook);
        Console.WriteLine("Book added successfully. ");
      }
    }

    public static void SearchBook(LibraryClass library)
    {
      Console.Write("Write a keyword for searching writer or the name of the book: ");
      string keyword = Console.ReadLine();
      library.BookSearch(keyword);
    }

    public static void ShowExpiredDate(LibraryClass library)
    {
      library.ShowExpiredBooks();
    }

    static void BorrowBook(LibraryClass library)
    {
      Console.Write("Type the title of the book that you want to borrow: ");
      string title = Console.ReadLine();
      Console.Write("Type the writer of the book that you want to borrow: ");
      string writer = Console.ReadLine();
      library.BorrowBook(title, writer);
    }

    static void ReturnTheBook(LibraryClass library)
    {
      Console.Write("Type the ISBN of the book that you want to return: ");
      string ISBN = Console.ReadLine();
      Console.Write("Type the Date of the book that you want to return: ");
      DateTime borrowDate = Convert.ToDateTime(Console.ReadLine());
      library.ReturnBook(ISBN, borrowDate);
    }
  }
}