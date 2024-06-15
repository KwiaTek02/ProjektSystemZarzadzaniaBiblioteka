using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektBiblioteka.Model;

namespace ProjektBiblioteka.Model
{
    public class Loan
    {
        public Reader Borrower { get; set; }
        public Book Book { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool IsReturned { get; set; }

        public Loan(Reader borrower, Book book, DateTime borrowDate, DateTime returnDate)
        {
            Borrower = borrower;
            Book = book;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
            IsReturned = false;
        }

        public string Display()
        {
            return $"{Book.Title} {Book.Author} - Data wypożyczenia: {BorrowDate}, Data zwrotu: {ReturnDate}";
        }



    }
}
