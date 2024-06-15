using System;
using System.Collections.Generic;
using ProjektBiblioteka.Enums;
using ProjektBiblioteka.Interfaces;
using ProjektBiblioteka.Projekt.Model;

namespace ProjektBiblioteka.Model
{
    public class Book : LibraryItem, ILibraryItem, ILoanable
    {
        public int Copies { get; set; }
        public List<Loan> Loans { get; set; }

        public Book(string title, string author,Categories category, int year, int copies) : base(title, author, category, year)
        {
            Copies = copies;
            Loans = new List<Loan>();
        }

        public string Display()
        {
            return $"{Title} {Author} Kategoria: {Category} ({Year}) - L.egz: {Copies}";
        }

        public bool IsAvailable()
        {
            return Copies > 0;
        }


        public void Borrow(Reader borrower, DateTime borrowDate, DateTime returnDate)
        {
            if (IsAvailable())
            {
                Copies--;
                Loans.Add(new Loan(borrower, this, borrowDate, returnDate));
                Console.WriteLine("Wypożyczenie zostało pomyślnie ustawione.");
            }
            else
            {
                Console.WriteLine("Książki nie można wypożyczyć.");
            }
        }

        public void Return(Loan loan)
        {
            Loans.Remove(loan);
            Copies++;
        }
    }
}
