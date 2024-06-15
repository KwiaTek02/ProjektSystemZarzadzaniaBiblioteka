using System;
using System.Collections.Generic;
using ProjektBiblioteka.Projekt.Model;

namespace ProjektBiblioteka.Model
{
    public class Reader : Person

    {
        public string PESEL { get; set; }
        public List<Loan> LoanHistory { get; set; }

        public Reader(string firstName, string lastName, string pesel)
            : base(firstName, lastName)
        {
            PESEL = pesel;
            LoanHistory = new List<Loan>();
        }

        public void AddToHistory(Loan loan)
        {
            LoanHistory.Add(loan);
        }

        public List<Loan> GetLoanHistory()
        {
            return LoanHistory;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} - PESEL: {PESEL}";
        }

        public void WyświetlHistorieUzytkownika()
        {
            Console.WriteLine($"Historia wypożyczeń dla czytelnika z PESEL'em: {PESEL}");
            foreach (var loan in LoanHistory)
            {
                Console.WriteLine(
                    $"{loan.Book.Title} {loan.Book.Author} - Data wypożyczenia: {loan.BorrowDate}, Data zwrotu: {loan.ReturnDate}");
            }
        }

    }
}
