using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfServiceDemo
{
    [DataContract]
    public class Book
    {
        [DataMember]
        public int BookId { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string ISBN { get; set; }
    }

    public interface IBookRepository
    {
        List<Book> GetAllBooks();
        Book GetBookById(int id);
        Book AddNewBook(Book item);
        bool DeleteABook(int id);
        bool UpdateABook(Book item);
    }

    public class BookRepository : IBookRepository
    {
        private List<Book> books = new List<Book>();
        private int counter = 1;

        public BookRepository()
        {
            AddNewBook(new Book { Title = "C# Programming", ISBN = "123" });
            AddNewBook(new Book { Title = "Java Programming", ISBN = "456" });
            AddNewBook(new Book { Title = "Wfc Programming", ISBN = "789" });
        }

        public Book AddNewBook(Book newBook)
        {
            if (newBook == null)
                throw new ArgumentNullException("newBook");
            newBook.BookId = counter++;
            books.Add(newBook);
            return newBook;
        }

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public Book GetBookById(int bookId)
        {
            return books.Find(b => b.BookId == bookId);
        }

        public bool UpdateABook(Book updatedBook)
        {
            if (updatedBook == null)
                throw new ArgumentNullException("updateBook");

            int idx = books.FindIndex(b => b.BookId == updatedBook.BookId);
            if (idx == -1)
                return false;
            books.RemoveAt(idx);
            books.Add(updatedBook);
            return true;
        }

        public bool DeleteABook(int bookId)
        {
            int idx = books.FindIndex(b => b.BookId == bookId);
            if (idx == -1)
                return false;
            books.RemoveAll(b => b.BookId == bookId);
            return true;
        }
    }

    
}