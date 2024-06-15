using ProjektBiblioteka.Model;
using System;


namespace ProjektBiblioteka.Interfaces
{
    internal interface ILibraryItem
    {
        bool IsAvailable();
        void Borrow(Reader borrower, DateTime borrowDate, DateTime returnDate);
        void Return(Loan loan);
        string Display();
    }
}
