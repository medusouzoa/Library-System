using System;

namespace LibrarySystem.Scripts
{
  public class BookClass
  {
    public string title { get; set; }
    public string writer { get; set; }
    public string ISBN { get; set; }
    public int copyCount { get; set; }
    public int borrowedCopies { get; set; }

    public DateTime borrowDate { get; set; }
  }
}