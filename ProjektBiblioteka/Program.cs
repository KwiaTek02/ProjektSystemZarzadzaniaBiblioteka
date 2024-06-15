using ProjektBiblioteka.Builders;
using ProjektBiblioteka.Managers;
using ProjektBiblioteka.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjektBiblioteka.Enums;

namespace ProjektBiblioteka
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LibraryManager libraryManager = new LibraryManager();

            Reader czytelnik1 = new ReaderBuilder()
                .SetFirstName("Mateusz")
                .SetLastName("Kwiatkowski")
                .SetPESEL("02302502794")
                .Build();

            Reader czytelnik2 = new ReaderBuilder()
                .SetFirstName("Kacper")
                .SetLastName("Bogunia")
                .SetPESEL("02356267456")
                .Build();
            Reader czytelnik3 = new ReaderBuilder()
                .SetFirstName("Adrianna")
                .SetLastName("Ambroziak")
                .SetPESEL("02182746257")
                .Build();

            Reader czytelnik4 = new ReaderBuilder()
                .SetFirstName("Filip")
                .SetLastName("Sobczak")
                .SetPESEL("02137147925")
                .Build();

            libraryManager.AddReader(czytelnik1);
            libraryManager.AddReader(czytelnik2);
            libraryManager.AddReader(czytelnik3);
            libraryManager.AddReader(czytelnik4);

            Book ksiazka1 = new BookBuilder()
                .SetTitle("Harry Potter i Kamień Filozoficzny")
                .SetAuthor("J.K.Rowling")
                .SetCategory(Categories.Fantasy)
                .SetYear(1997)
                .SetCopies(3)
                .Build();

            Book ksiazka2 = new BookBuilder()
                .SetTitle("Harry Potter i Komnata Tajemnic")
                .SetAuthor("J.K.Rowling")
                .SetCategory(Categories.Fantasy)
                .SetYear(1998)
                .SetCopies(0)
                .Build();

            Book ksiazka3 = new BookBuilder()
                .SetTitle("Harry Potter i Więzień Azkabanu")
                .SetAuthor("J.K.Rowling")
                .SetCategory(Categories.Fantasy)
                .SetYear(1999)
                .SetCopies(2)
                .Build();

            Book ksiazka4 = new BookBuilder()
                .SetTitle("Harry Potter i Czara Ognia")
                .SetAuthor("J.K.Rowling")
                .SetCategory(Categories.Fantasy)
                .SetYear(2000)
                .SetCopies(5)
                .Build();

            Book ksiazka5 = new BookBuilder()
                .SetTitle("Przyjaciółka")
                .SetAuthor("B. A. Paris")
                .SetCategory(Categories.Thriller)
                .SetYear(2024)
                .SetCopies(3)
                .Build();

            Book ksiazka6 = new BookBuilder()
                .SetTitle("Uwięziona")
                .SetAuthor("B. A. Paris")
                .SetCategory(Categories.Thriller)
                .SetYear(2023)
                .SetCopies(9)
                .Build();

            Book ksiazka7 = new BookBuilder()
                .SetTitle("Carmilla")
                .SetAuthor("Le Fanu Joseph Sheridan")
                .SetCategory(Categories.Horror)
                .SetYear(2024)
                .SetCopies(1)
                .Build();
            libraryManager.AddBook(ksiazka1);
            libraryManager.AddBook(ksiazka2);
            libraryManager.AddBook(ksiazka3);
            libraryManager.AddBook(ksiazka4);
            libraryManager.AddBook(ksiazka5);
            libraryManager.AddBook(ksiazka6);
            libraryManager.AddBook(ksiazka7);

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nGŁÓWNE MENU");
                Console.ResetColor();
                Console.WriteLine("\n1. Czytelnicy");
                Console.WriteLine("2. Ksiażki");
                Console.WriteLine("3. Wypożyczenia");
                Console.WriteLine("4. Wyjście\n");
                Console.Write("Wybierz opcje (1-4): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        ReaderMenu(libraryManager);
                        break;
                    case "2":
                        Console.Clear();
                        BookMenu(libraryManager);
                        break;
                    case "3":
                        Console.Clear();
                        LoanMenu(libraryManager);
                        break;
                    case "4":
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Nieprawidłowa opcja. Wybierz ponownie.");
                        break;
                }
            }
        }

        static void ReaderMenu(LibraryManager libraryManager)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nMenu Czytelnicy");
                Console.ResetColor();
                Console.WriteLine("\n1. Wyświetl wszystkich czytelników");
                Console.WriteLine("2. Dodaj nowego czytelnika");
                Console.WriteLine("3. Usuń czytelnika");
                Console.WriteLine("4. Edytuj czytelnika");
                Console.WriteLine("5. Wróc go głównego menu\n");
                Console.Write("Wybierz opcje (1-5): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        libraryManager.DisplayReaders();
                        break;
                    case "2":
                        Console.Clear();
                        AddReader(libraryManager);
                        break;
                    case "3":
                        Console.Clear();
                        libraryManager.DisplayReaders();
                        RemoveReader(libraryManager);
                        break;
                    case "4":
                        Console.Clear();
                        libraryManager.DisplayReaders();
                        EditReader(libraryManager);
                        break;
                    case "5":
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Nieprawidłowa opcja. Wybierz ponownie.");
                        break;
                }
            }
        }

        static void AddReader(LibraryManager libraryManager)
        {
            Console.WriteLine("\nPodaj dane czytelnika:");
            Console.Write("Imię: ");
            string firstName = Console.ReadLine();
            Console.Write("Nazwisko: ");
            string lastName = Console.ReadLine();

            string pesel;
            bool isValidPesel = false;
            do
            {
                Console.Write("PESEL (11 cyfr): ");
                pesel = Console.ReadLine();
                isValidPesel = IsAllDigits(pesel) && pesel.Length == 11;
                if (!isValidPesel)
                {
                    Console.WriteLine("Nieprawidłowy PESEL. PESEL musi składać się z 11 cyfr.");
                }
            } while (!isValidPesel);

            Reader newReader = new ReaderBuilder()
                .SetFirstName(firstName)
                .SetLastName(lastName)
                .SetPESEL(pesel)
                .Build();

            libraryManager.AddReader(newReader);
            Console.WriteLine("Czytelnik został pomyślnie dodany.");
        }

        static bool IsAllDigits(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        static void RemoveReader(LibraryManager libraryManager)
        {
            Console.WriteLine("\nWpisz PESEL czytelnika, aby go usunąć:");
            string pesel = Console.ReadLine();
            Reader reader = libraryManager.GetReaderByPESEL(pesel);
            if (reader == null)
            {
                Console.WriteLine("Nie znaleziono czytelnika.");
            }
            else
            {
                libraryManager.RemoveReader(reader);
                Console.WriteLine("Czytelnik został pomyślnie usunięty.");
            }
        }

        static void EditReader(LibraryManager libraryManager)
        {
            Console.WriteLine("\nWpisz PESEL czytelnika do edycji:");
            string pesel = Console.ReadLine();
            Reader reader = libraryManager.GetReaderByPESEL(pesel);
            if (reader == null)
            {
                Console.WriteLine("Nie znaleziono czytelnika.");
            }
            else
            {
                Console.WriteLine("Wpisz nowe imię:");
                string newFirstName = Console.ReadLine();
                Console.WriteLine("Wpisz nowe nazwisko:");
                string newLastName = Console.ReadLine();
                libraryManager.EditReader(pesel, newFirstName, newLastName);
                Console.WriteLine("Czytelnik edytowany pomyślnie.");
            }
        }

        static void BookMenu(LibraryManager libraryManager)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nMenu Książki");
                Console.ResetColor();
                Console.WriteLine("\n1. Wyświetl wszystkie książki");
                Console.WriteLine("2. Wyświetl wszystkie kategorie");
                Console.WriteLine("3. Dodaj nową książkę");
                Console.WriteLine("4. Usuń książke");
                Console.WriteLine("5. Edytuj Książke");
                Console.WriteLine("6. Wyszukaj książke");
                Console.WriteLine("7. Wróć do głównego menu\n");
                Console.Write("Wybierz opcje (1-7): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        libraryManager.DisplayBooks();
                        break;
                    case "2":
                        Console.Clear();
                        DisplayAllCategories(libraryManager);
                        break;
                    case "3":
                        Console.Clear();
                        AddBook(libraryManager);
                        break;
                    case "4":
                        Console.Clear();
                        RemoveBook(libraryManager);
                        break;
                    case "5":
                        Console.Clear();
                        EditBook(libraryManager);
                        break;
                    case "6":
                        Console.Clear();
                        SearchBook(libraryManager);
                        break;
                    case "7":
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Nieprawidłowa opcja. Wybierz ponownie.");
                        break;
                }
            }
        }


        static void AddBook(LibraryManager libraryManager)
        {
            Console.WriteLine("\nPodaj szczegóły książki:");
            Console.Write("Tytuł: ");
            string title = Console.ReadLine();
            Console.Write("Autor: ");
            string author = Console.ReadLine();

            int year;
            bool isValidYear = false;
            do
            {
                Console.Write("Rok wydania: ");
                string yearInput = Console.ReadLine();
                isValidYear = int.TryParse(yearInput, out year);
                if (!isValidYear)
                {
                    Console.WriteLine("Nieprawidłowe dane wejściowe. Proszę wpisać poprawny rok.");
                }
            } while (!isValidYear);

            int copies;
            bool isValidCopies = false;
            do
            {
                Console.Write("L.egz: ");
                string copiesInput = Console.ReadLine();
                isValidCopies = int.TryParse(copiesInput, out copies);
                if (!isValidCopies)
                {
                    Console.WriteLine("Nieprawidłowe dane wejściowe. Proszę wprowadzić prawidłową liczbę kopii.");
                }
            } while (!isValidCopies);

            Console.WriteLine("Dostępne kategorie:");
            Console.WriteLine("1. Thriller");
            Console.WriteLine("2. Horror");
            Console.WriteLine("3. Fantasy");

            int categoryChoice;
            bool isValidCategory = false;
            do
            {
                Console.Write("Wybierz kategorie (1-3): ");
                string categoryInput = Console.ReadLine();
                isValidCategory = int.TryParse(categoryInput, out categoryChoice);
                if (!isValidCategory || categoryChoice < 1 || categoryChoice > 3)
                {
                    Console.WriteLine("Nieprawidłowe dane wejściowe. Wybierz liczbę od 1 do 3.");
                    isValidCategory = false;
                }
            } while (!isValidCategory);

            Categories category = (Categories)(categoryChoice - 1);

            Book newBook = new BookBuilder()
                .SetTitle(title)
                .SetAuthor(author)
                .SetYear(year)
                .SetCopies(copies)
                .SetCategory(category)
                .Build();

            libraryManager.AddBook(newBook);
            Console.WriteLine("Książka została dodana pomyślnie. ");
        }


        static void RemoveBook(LibraryManager libraryManager)
        {

            libraryManager.RemoveBook();
        }


        static void EditBook(LibraryManager libraryManager)
        {
            libraryManager.EditBook();
        }

        static void SearchBook(LibraryManager libraryManager)
        {
            Console.WriteLine("\nWyszukaj książkę:");
            Console.WriteLine("1. Tytuł");
            Console.WriteLine("2. Autor");
            Console.WriteLine("3. Rok wydania");
            Console.Write("Wybierz opcję (1-3): ");
            string searchOption = Console.ReadLine();

            List<Book> foundBooks = libraryManager.SearchBook(searchOption, libraryManager.GetBooks());

            if (foundBooks.Count > 0)
            {
                Console.WriteLine("\nZnaleziono książki:");
                foreach (var book in foundBooks)
                {
                    Console.WriteLine(book.Display());
                }
            }
            else
            {
                Console.WriteLine("Nie znaleziono książek.");
            }

        }

        static void DisplayAllCategories(LibraryManager libraryManager)
        {
            libraryManager.DisplayAllCategories();
        }

        static void LoanMenu(LibraryManager libraryManager)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nMenu Wypożyczeń");
                Console.ResetColor();
                Console.WriteLine("\n1. Wyświetl wszystkie wypożyczenia");
                Console.WriteLine("2. Ustaw wypożyczenie");
                Console.WriteLine("3. Zwróć wypożyczenie");
                Console.WriteLine("4. Wyświetl historie wypożyczeń użytkownika");
                Console.WriteLine("5. Przedłuż wypożyczenie");
                Console.WriteLine("6. Wróć do głównego menu\n");

                Console.Write("Wybierz opcję (1-6): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        libraryManager.DisplayLoans();
                        break;
                    case "2":
                        Console.Clear();
                        libraryManager.DisplayReaders();
                        SetLoan(libraryManager);
                        break;
                    case "3":
                        Console.Clear();
                        libraryManager.DisplayReaders();
                        ReturnLoan(libraryManager);
                        break;
                    case "4":
                        Console.Clear();
                        libraryManager.DisplayReaders();
                        Console.WriteLine("");
                        LoanHistory(libraryManager);
                        break;
                    case "5":
                        Console.Clear();
                        libraryManager.DisplayReaders();
                        Console.WriteLine("");
                        ExtensionLoan(libraryManager);
                        break;
                    case "6":
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Nieprawidłowa opcja. Wybierz ponownie.");
                        break;
                }
            }
        }

        static void SetLoan(LibraryManager libraryManager)
        {
            Console.WriteLine("\nPodaj szczegóły wypożyczenia:");
            Console.Write("PESEL czytelnika: ");
            string pesel = Console.ReadLine();
            Reader reader = libraryManager.GetReaderByPESEL(pesel);
            if (reader == null)
            {
                Console.WriteLine("Nie znaleziono czytelnika.");
                return;
            }

            Console.Write("Tytuł książki (lub fragment): ");
            string partialTitle = Console.ReadLine();
            List<Book> matchingBooks =
                libraryManager.GetBooks().Where(book => book.Title.Contains(partialTitle)).ToList();
            if (matchingBooks.Count == 0)
            {
                Console.WriteLine("Nie znaleziono książek pasujących do tytułu.");
                return;
            }
            
            Console.WriteLine("Wybierz książkę do wypożyczenia:");
            for (int i = 0; i < matchingBooks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {matchingBooks[i].Title} by {matchingBooks[i].Author} ({matchingBooks[i].Category}) ({matchingBooks[i].Year}) L.egz:{matchingBooks[i].Copies} ");
            }

            Console.Write("Podaj numer książki do wypożyczenia: ");
            if (!int.TryParse(Console.ReadLine(), out int selectedBookIndex) || selectedBookIndex < 1 ||
                selectedBookIndex > matchingBooks.Count)
            {
                Console.WriteLine("Nieprawidłowy wybór.");
                return;
            }

            Book selectedBook = matchingBooks[selectedBookIndex - 1];

            Console.Write("Podaj liczbę dni wypożyczenia: ");
            if (!int.TryParse(Console.ReadLine(), out int daysToLoan) || daysToLoan <= 0)
            {
                Console.WriteLine("Nieprawidłowa liczba dni.");
                return;
            }

            DateTime borrowDate = DateTime.Now;
            DateTime returnDate = borrowDate.AddDays(daysToLoan);

            if (selectedBook.Copies > 0)
            {
                libraryManager.SetLoan(reader, selectedBook, borrowDate, returnDate);

            }

            if (selectedBook.Copies == 0)
            {
                Console.WriteLine("Książki nie można wypożyczyć.");
            }
            
        }




        static void ReturnLoan(LibraryManager libraryManager)
        {
            Console.WriteLine("\nWprowadź dane wypożyczenia do zwrotu:");
            Console.Write("PESEL czytelnika: ");
            string pesel = Console.ReadLine();
            Reader reader = libraryManager.GetReaderByPESEL(pesel);
            if (reader == null)
            {
                Console.WriteLine("Nie znaleziono czytnika.");
                return;
            }

            Console.Write("Wpisz tytuł (lub fragment): ");
            string partialTitle = Console.ReadLine();

            libraryManager.ReturnLoan(reader, partialTitle);

        }

        static void LoanHistory(LibraryManager libraryManager)
        {
            Console.Write("Wpisz PESEL czytelnika: ");
            string pesel = Console.ReadLine();
            libraryManager.GetLoanHistory(pesel);

        }

        static void ExtensionLoan(LibraryManager libraryManager)
        {
            libraryManager.ExtensionLoan();
        }
    }
}
