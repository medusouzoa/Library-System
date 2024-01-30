using System;
using System.Collections.Generic;
using System.IO;

namespace LibrarySystem.Scripts
{
  public class LibraryClass
  {
    public List<BookClass> bookList = new List<BookClass>();
    public List<BookClass> borrowedBooks = new List<BookClass>();
    private string filename = "library.txt";
    private string bbFile = "borrowedBooks.txt";

    public void AddBook(BookClass book)
    {
      bookList.Add(book);
      SaveTheBooks();
    }

    public void ListTheBooks()
    {
      foreach (BookClass book in bookList)
      {
        Console.WriteLine($"Title: {book.title}, Writer: {book.writer}, ISBN: {book.ISBN}," +
                          $" Copy Count: {book.copyCount}," +
                          $" Borrowed Books: {book.borrowedCopies}");
      }
    }

    public void ListTheBorrowedBooks()
    {
      foreach (BookClass book in borrowedBooks)
      {
        Console.WriteLine($"Title: {book.title}, Writer: {book.writer}, ISBN: {book.ISBN}," +
                          $" Copy Count: {book.borrowDate}");
      }
    }

    public BookClass ExactBookSearch(string ISBN)
    {
      List<BookClass> foundBooks = bookList.FindAll(k => k.ISBN.Contains(ISBN));
      if (foundBooks.Count > 0)
      {
        return foundBooks[0];
      }

      return null;
    }

    public void BookSearch(string bookWriter)
    {
      List<BookClass> foundBooks = bookList.FindAll(k => k.title.Contains(bookWriter) ||
                                                         k.writer.Contains(bookWriter));

      if (foundBooks.Count > 0)
      {
        foreach (BookClass book in foundBooks)
        {
          Console.WriteLine($"Title: {book.title}, Writer: {book.writer}, " +
                            $"ISBN: {book.ISBN}, Copy Count: {book.copyCount}, " +
                            $"Borrowed Books Count: {book.borrowedCopies}");
        }
      }
      else
      {
        Console.WriteLine("No books found");
      }
    }

    private bool IsDateExpired(BookClass book)
    {
      DateTime today = DateTime.Now;
      DateTime borrowDate = book.borrowDate;

      TimeSpan difference = today - borrowDate;

      return difference.Days >= 15;
    }

    public void BorrowBook(string title, string writer)
    {
      BookClass book = bookList.Find(k => k.title.Equals(title) && k.writer.Equals(writer));

      if (book != null)
      {
        if (book.copyCount > book.borrowedCopies)
        {
          book.borrowedCopies++;
          BookClass newBook = new BookClass
          {
            title = title,
            writer = writer,
            ISBN = book.ISBN,
            borrowDate = DateTime.Now.Date,
          };
          borrowedBooks.Add(newBook);
          SaveTheBorrowedBooks();
          SaveTheBooks();
          Console.WriteLine($"You borrowed the book {title}");
        }
        else
        {
          Console.WriteLine("No enough copies.");
        }
      }
      else
      {
        Console.WriteLine("Book couldn't find");
      }
    }

    public void ReturnBook(string ISBN, DateTime bDate)
    {
      BookClass book = borrowedBooks.Find(k => k.ISBN.Equals(ISBN) &&
                                               k.borrowDate.Equals(bDate.Date));

      if (book != null)
      {
        BookClass rebook = ExactBookSearch(ISBN);
        rebook.borrowedCopies--;
        borrowedBooks.Remove(book);
        SaveTheBorrowedBooks();
        SaveTheBooks();
        Console.WriteLine($"The book titled {book.title} was returned.");
      }
      else
      {
        Console.WriteLine("The book doesn't exist or has never been borrowed. ");
      }
    }

    public void SaveTheBooks()
    {
      using (StreamWriter writer = new StreamWriter(filename))
      {
        foreach (BookClass book in bookList)
        {
          writer.WriteLine($"{book.title},{book.writer},{book.ISBN}," +
                           $"{book.copyCount},{book.borrowedCopies}");
          Console.WriteLine("book wrote to the file.");
        }
      }
    }

    public void SaveTheBorrowedBooks()
    {
      using (StreamWriter writer = new StreamWriter(bbFile))
      {
        foreach (BookClass book in borrowedBooks)
        {
          writer.WriteLine($"{book.title},{book.writer},{book.ISBN},{book.borrowDate}");
          Console.WriteLine("book wrote to the file.");
        }
      }
    }

    public void ShowExpiredBooks()
    {
      List<BookClass> expiredBooks = borrowedBooks.FindAll(k => (IsDateExpired(k)));

      if (expiredBooks.Count > 0)
      {
        Console.WriteLine("Expired books: ");
        foreach (BookClass book in expiredBooks)
        {
          Console.WriteLine($"Title: {book.title}, Writer: {book.writer}, ISBN: {book.ISBN}, " +
                            $"Borrow Date: {book.borrowDate}");
        }
      }
      else
      {
        Console.WriteLine("There is no expired dated books.");
      }
    }

    public void InsertBooks()
    {
      if (File.Exists(filename))
      {
        bookList.Clear();
        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
          string[] fields = line.Split(',');

          BookClass book = new BookClass
          {
            title = fields[0],
            writer = fields[1],
            ISBN = fields[2],
            copyCount = int.Parse(fields[3]),
            borrowedCopies = int.Parse(fields[4])
          };

          bookList.Add(book);
        }
      }
    }

    public void InsertBorrowedBooks()
    {
      if (File.Exists(bbFile))
      {
        borrowedBooks.Clear();
        string[] lines = File.ReadAllLines(bbFile);

        foreach (string line in lines)
        {
          string[] fields = line.Split(',');
          BookClass book = new BookClass
          {
            title = fields[0],
            writer = fields[1],
            ISBN = fields[2],
            borrowDate = DateTime.Parse((fields[3])).Date,
          };

          borrowedBooks.Add(book);
        }
      }
    }
  }
}