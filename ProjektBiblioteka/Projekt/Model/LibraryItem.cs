using ProjektBiblioteka.Enums;

namespace ProjektBiblioteka.Projekt.Model
{
    public class LibraryItem 
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public Categories Category { get; set; }

        public LibraryItem(string title, string author, Categories category, int year)
        {
            Title = title;
            Author = author;
            Category = category;
            Year = year;
        }

        public string Display()
        {
            return $"{Title} {Author} Kategoria: {Category} ({Year})";
        }

    }
}
