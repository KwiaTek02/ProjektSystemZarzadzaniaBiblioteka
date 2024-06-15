using ProjektBiblioteka.Model;
using System;
using System.Collections.Generic;
using ProjektBiblioteka.Enums;

namespace ProjektBiblioteka.Interfaces
{
    public interface IBookBuilder
    {
        IBookBuilder SetTitle(string title);
        IBookBuilder SetAuthor(string author);
        IBookBuilder SetYear(int year);
        IBookBuilder SetCategory(Categories categories);
        IBookBuilder SetCopies(int copies);
        Book Build();
        IBookBuilder BorrowBook(Reader borrower, DateTime borrowDate, DateTime returnDate);
        IBookBuilder ReturnBook(Loan loan);
        List<Book> SearchByTitle(List<Book> books, string partialTitle);
        List<Book> SearchByAuthor(List<Book> books, string partialAuthor);
        List<Book> SearchByYear(List<Book> books, int year);



    }
}
