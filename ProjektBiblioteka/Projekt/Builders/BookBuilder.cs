using System;
using System.Collections.Generic;
using System.Linq;
using ProjektBiblioteka.Enums;
using ProjektBiblioteka.Interfaces;
using ProjektBiblioteka.Model;
// ReSharper disable NotAccessedField.Local

namespace ProjektBiblioteka.Builders
{
    public class BookBuilder: IBookBuilder
    {
        private string _title;
        private string _author;
        private int _year;
        private Categories _category;
        private int _copies;
        private Reader _borrower;
        private DateTime _borrowDate;
        private DateTime _returnDate;

        public IBookBuilder SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public IBookBuilder SetAuthor(string author)
        {
            _author = author;
            return this;
        }

        public IBookBuilder SetYear(int year)
        {
            _year = year;
            return this;
        }

        public IBookBuilder SetCopies(int copies)
        {
            _copies = copies;
            return this;
        }

        public IBookBuilder SetCategory(Categories category)
        {
            _category = category;
            return this;
        }
        public List<Book> SearchByTitle(List<Book> books, string partialTitle)
        {
            return books.Where(book => book.Title.Contains(partialTitle)).ToList();
        }

        public List<Book> SearchByAuthor(List<Book> books, string partialAuthor)
        {
            return books.Where(book => book.Author.Contains(partialAuthor)).ToList();
        }
        public List<Book> SearchByYear(List<Book> books, int year)
        {
            return books.Where(book => book.Year == year).ToList();
        }

        public Book Build()
        {
            return new Book(_title, _author, _category, _year, _copies);
        }

        public IBookBuilder BorrowBook(Reader borrower, DateTime borrowDate, DateTime returnDate)
        {
            _borrower = borrower;
            _borrowDate = borrowDate;
            _returnDate = returnDate;
            return this;
        }

        public IBookBuilder ReturnBook(Loan loan)
        {
            loan.Book.Return(loan);
            return this;
        }
    }
}
