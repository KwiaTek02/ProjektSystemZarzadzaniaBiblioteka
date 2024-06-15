using ProjektBiblioteka.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjektBiblioteka.Builders;
using ProjektBiblioteka.Enums;


namespace ProjektBiblioteka.Managers
{
    public class LibraryManager
    {
        private List<Reader> readers = new List<Reader>();
        private List<Book> books = new List<Book>();
        private List<Loan> loans = new List<Loan>();

        public LibraryManager()
        {
            books = new List<Book>();
            readers = new List<Reader>();
            loans = new List<Loan>(); 
        }

        public void DisplayReaders()
        {
            Console.WriteLine("\nCzytelnicy:");
            foreach (var reader in readers)
            {
                Console.WriteLine(reader);
            }
        }
        public void AddReader(Reader reader)
        {
            readers.Add(reader);
        }

        public void RemoveReader(Reader reader)
        {
            readers.Remove(reader);
        }

        public void EditReader(string pesel, string newFirstName, string newLastName)
        {
            var reader = readers.FirstOrDefault(r => r.PESEL == pesel);
            if (reader != null)
            {
                reader.FirstName = newFirstName;
                reader.LastName = newLastName;
            }
        }
        public void AddBook(Book book)
        {

            books.Add(book);
        }
        

        public void DisplayBooks()
        {
            Console.WriteLine("\nKsiążki:");
            foreach (var book in books)
            {
                Console.WriteLine(book.Display());
            }
        }

        public void RemoveBook()
        {
            Console.WriteLine("\nUsuń książkę:");
            Console.WriteLine("Wyszukaj książke poprzez:");
            Console.WriteLine("1. Tytuł");
            Console.WriteLine("2. Autor");
            Console.WriteLine("3. Rok wydania");
            Console.Write("Wybierz opcję (1-3): ");
            string searchOption = Console.ReadLine();

            List<Book> foundBooks = SearchBook(searchOption, GetBooks());

            if (foundBooks.Count == 0)
            {
                Console.WriteLine("Nie znaleziono książek.");
                return;
            }

            Console.WriteLine("Wybierz książkę do usunięcia::");
            for (int i = 0; i < foundBooks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. „{foundBooks[i].Title}” {foundBooks[i].Author} ({foundBooks[i].Category}) ({foundBooks[i].Year}) L.egz: {foundBooks[i].Copies}");
            }

            Console.Write("Podaj numer książki do usunięcia: ");
            if (!int.TryParse(Console.ReadLine(), out int selectedBookIndex) || selectedBookIndex < 1 || selectedBookIndex > foundBooks.Count)
            {
                Console.WriteLine("Nieprawidłowy wybór.");
                return;
            }

            Book selectedBook = foundBooks[selectedBookIndex - 1];
            GetBooks().Remove(selectedBook);
            Console.WriteLine("Książka została pomyślnie usunięta.");
        }

        public void EditBook()
        {
            Console.WriteLine("\nEdytuj książkę:");
            Console.WriteLine("Wyszukaj książkę poprzez:");
            Console.WriteLine("1. Tytuł");
            Console.WriteLine("2. Auhor");
            Console.WriteLine("3. Rok wydania");
            Console.Write("Wybierz opcję (1-3): ");
            string searchOption = Console.ReadLine();

            List<Book> foundBooks = SearchBook(searchOption, GetBooks());

            if (foundBooks.Count == 0)
            {
                Console.WriteLine("Nie znaleziono książek.");
                return;
            }

            Console.WriteLine("Wybierz książkę do edycji:");
            for (int i = 0; i < foundBooks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. „{foundBooks[i].Title}” {foundBooks[i].Author} ({foundBooks[i].Category}) ({foundBooks[i].Year}) L.egz: {foundBooks[i].Copies}");
            }

            Console.Write("Podaj numer książki do edycji: ");
            if (!int.TryParse(Console.ReadLine(), out int selectedBookIndex) || selectedBookIndex < 1 || selectedBookIndex > foundBooks.Count)
            {
                Console.WriteLine("Nieprawidłowy wybór.");
                return;
            }

            Book selectedBook = foundBooks[selectedBookIndex - 1];

            Console.Write("Wprowadź nowy tytuł: ");
            string newTitle = Console.ReadLine();
            selectedBook.Title = newTitle;

            Console.Write("Wprowadź nowego autora: ");
            string newAuthor = Console.ReadLine();
            selectedBook.Author = newAuthor;

            Console.WriteLine("Wybierz nową kategorię:");
            int categoryIndex = 1;
            foreach (Categories category in Enum.GetValues(typeof(Categories)))
            {
                Console.WriteLine($"{categoryIndex}. {category}");
                categoryIndex++;
            }
            Console.Write("Wybierz opcję (1-3): ");
            if (!int.TryParse(Console.ReadLine(), out int selectedCategoryIndex) || selectedCategoryIndex < 1 || selectedCategoryIndex > Enum.GetValues(typeof(Categories)).Length)
            {
                Console.WriteLine("Nieprawidłowy wybór kategorii.");
                return;
            }
            selectedBook.Category = ((Categories)(selectedCategoryIndex - 1));

            Console.Write("Wprowadź nowy rok: ");
            if (!int.TryParse(Console.ReadLine(), out int newYear))
            {
                Console.WriteLine("Nieprawidłowy rok.");
                return;
            }
            selectedBook.Year = newYear;

            Console.Write("Wprowadź nową liczbę kopii: ");
            if (!int.TryParse(Console.ReadLine(), out int newCopies))
            {
                Console.WriteLine("Nieprawidłowa liczba kopii.");
                return;
            }
            selectedBook.Copies = newCopies;

            Console.WriteLine("Książka została pomyślnie edytowana.");

            UpdateBooksList(foundBooks);
        }

        private void UpdateBooksList(List<Book> updatedBooks)
        {
            foreach (var book in updatedBooks)
            {
                int index = books.FindIndex(b => b.Title == book.Title && b.Author == book.Author);
                if (index != -1)
                {
                    books[index] = book;
                }
            }
        }


        public List<Book> SearchBook(string searchOption, List<Book> books)
        {
            BookBuilder bookBuilder = new BookBuilder();

            switch (searchOption)
            {
                case "1":
                    Console.Write("Wpisz tytuł (lub fragment):: ");
                    string partialTitle = Console.ReadLine();
                    return bookBuilder.SearchByTitle(books, partialTitle);
                case "2":
                    Console.Write("Wprowadź autora: ");
                    string partialAuthor = Console.ReadLine();
                    return bookBuilder.SearchByAuthor(books, partialAuthor);
                case "3":
                    int year;
                    bool isValidYear = false;
                    do
                    {
                        Console.Write("Wprowadź rok: ");
                        string yearInput = Console.ReadLine();
                        isValidYear = int.TryParse(yearInput, out year);
                        if (!isValidYear)
                        {
                            Console.WriteLine("Nieprawidłowe dane wejściowe. Proszę wpisać poprawny rok.");
                        }
                    } while (!isValidYear);

                    return bookBuilder.SearchByYear(books, year);
                default:
                    Console.WriteLine("Nieprawidłowa opcja.");
                    return new List<Book>();
            }
        }

        public List<Book> GetBooks()
        {
            return books;
        }

        public void DisplayAllCategories()
        {
            Console.WriteLine("\nWszystkie kategorie:");
            int index = 1;
            foreach (var category in Enum.GetValues(typeof(Categories)))
            {
                Console.WriteLine($"{index}. {category}");
                index++;
            }
        }



        public void DisplayLoans()
        {
            if (loans.Count == 0)
            {
                Console.WriteLine("Brak wypożyczeń.");
                return;
            }

            Console.WriteLine("Lista wszystkich wypożyczeń:");
            foreach (var loan in loans)
            {
                Console.WriteLine($"Wypożyczający: {loan.Borrower.FirstName} {loan.Borrower.LastName}, Książka: {loan.Book.Title} {loan.Book.Author}, Data wypożyczenia: {loan.BorrowDate}, Data zwrotu: {loan.ReturnDate}");
            }
        }

        public void SetLoan(Reader reader, Book book, DateTime borrowDate, DateTime returnDate)
        {
            if (book.Copies > 0)
            {
                book.Borrow(reader, borrowDate, returnDate);
                reader.AddToHistory(new Loan(reader, book, borrowDate, returnDate));
                loans.Add(new Loan(reader, book, borrowDate, returnDate));
            }
        }

        public void ReturnLoan(Reader reader, string partialTitle)
        {
            List<Loan> userLoans = loans.Where(l => l.Borrower.PESEL == reader.PESEL).ToList();
            if (userLoans.Count == 0)
            {
                Console.WriteLine("Nie masz żadnych wypożyczeń do zwrotu.");
                return;
            }

            List<Loan> matchingLoans = userLoans.Where(l => l.Book.Title.Contains(partialTitle)).ToList();
            if (matchingLoans.Count == 0)
            {
                Console.WriteLine("Nie znaleziono wypożyczeń o tym tytule");
                return;
            }

            Console.WriteLine("Wybierz wypożyczenie do zwrotu:");
            for (int i = 0; i < matchingLoans.Count; i++)
            {
                Console.WriteLine($"{i + 1}. „{matchingLoans[i].Book.Title}” {matchingLoans[i].Book.Author} ({matchingLoans[i].Book.Category}) Rok wydania: {matchingLoans[i].Book.Year} L.egz: {matchingLoans[i].Book.Copies}");
            }

            Console.Write("Podaj numer wypożyczenia do zwrotu: ");
            if (!int.TryParse(Console.ReadLine(), out int selectedLoanIndex) || selectedLoanIndex < 1 || selectedLoanIndex > matchingLoans.Count)
            {
                Console.WriteLine("Nieprawidłowy wybór.");
                return;
            }

            Loan selectedLoan = matchingLoans[selectedLoanIndex - 1];

            if (loans.Contains(selectedLoan))
            {
                if (!selectedLoan.IsReturned)
                {
                    selectedLoan.Book.Return(selectedLoan);
                    selectedLoan.IsReturned = true;
                    loans.Remove(selectedLoan);
                    Console.WriteLine("Wypożyczenie zostało zwrócone pomyślnie.");
                }
                else
                {
                    Console.WriteLine("To wypożyczenie zostało już wcześniej zwrócone.");
                }
            }
            else
            {
                Console.WriteLine("Błąd: Wybranego wypożyczenia nie znaleziono na liście wypożyczeń.");
            }
        }


        public void GetLoanHistory(string pesel)
        {
            Reader reader = GetReaderByPESEL(pesel);
            if (reader == null)
            {
                Console.WriteLine("Nie znaleziono czytnika.");
                return;
            }

            Console.WriteLine($"Historia wypożyczeń dla czytelnika o numerze PESEL: {pesel}");

            var loanHistory = reader.GetLoanHistory().Where(loan => !loan.IsReturned);

            if (loanHistory.Count() == 0)
            {
                Console.WriteLine("Brak wypożyczeń.");
                return;
            }

            foreach (var loan in loanHistory)
            {
                Console.WriteLine(loan.Display());
            }
        }



        public void ExtensionLoan()
        {
            Console.Write("Wpisz PESEL czytelnika: ");
            string pesel = Console.ReadLine();
            Reader reader = GetReaderByPESEL(pesel);
            if (reader == null)
            {
                Console.WriteLine("Czytelnik nie znaleziony.");
                return;
            }

            List<Loan> userLoans = loans.Where(l => l.Borrower.PESEL == reader.PESEL && !l.IsReturned).ToList();
            if (userLoans.Count == 0)
            {
                Console.WriteLine("Nie masz żadnych aktywnych wypożyczeń.");
                return;
            }

            Console.WriteLine("\nTwoje aktywne wypożyczenia:");
            for (int i = 0; i < userLoans.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {userLoans[i].Display()}");
            }

            Console.Write("Wybierz wypożyczenie do przedłużenia (1-" + userLoans.Count + "): ");
            if (!int.TryParse(Console.ReadLine(), out int selectedLoanIndex) || selectedLoanIndex < 1 || selectedLoanIndex > userLoans.Count)
            {
                Console.WriteLine("Nieprawidłowy wybór.");
                return;
            }

            Loan selectedLoan = userLoans[selectedLoanIndex - 1];

            Console.Write("Wprowadź liczbę dni do przedłużenia: ");
            if (!int.TryParse(Console.ReadLine(), out int daysToExtend) || daysToExtend <= 0)
            {
                Console.WriteLine("Nieprawidłowa liczba dni.");
                return;
            }

            selectedLoan.ReturnDate = selectedLoan.ReturnDate.AddDays(daysToExtend);
            Console.WriteLine($"Wypożyczenie przedłużone pomyślnie. Nowa data powrotu: {selectedLoan.ReturnDate}");

            UpdateLoanReturnDate(selectedLoan);
        }


        public Reader GetReaderByPESEL(string pesel)
        {
            return readers.FirstOrDefault(r => r.PESEL == pesel);
        }

        private void UpdateLoanReturnDate(Loan loan)
        {
            int index = loans.FindIndex(l => l.Borrower.PESEL == loan.Borrower.PESEL && l.Book.Title == loan.Book.Title && !l.IsReturned);
            if (index != -1)
            {
                loans[index].ReturnDate = loan.ReturnDate;
            }
            
            Reader reader = GetReaderByPESEL(loan.Borrower.PESEL);
            if (reader != null)
            {
                int historyIndex = reader.LoanHistory.FindIndex(l => l.Borrower.PESEL == loan.Borrower.PESEL && l.Book.Title == loan.Book.Title);
                if (historyIndex != -1)
                {
                    reader.LoanHistory[historyIndex].ReturnDate = loan.ReturnDate;
                }
            }
        }

    }
}
