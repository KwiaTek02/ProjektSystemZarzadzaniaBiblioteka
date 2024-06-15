using ProjektBiblioteka.Model;

namespace ProjektBiblioteka.Interfaces
{
    public interface IReaderBuilder
    {
        
        IReaderBuilder SetFirstName(string firstName);
        IReaderBuilder SetLastName(string lastName);
        IReaderBuilder SetPESEL(string pesel);
        Reader Build();
    }
}
