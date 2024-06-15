using ProjektBiblioteka.Interfaces;
using ProjektBiblioteka.Model;

namespace ProjektBiblioteka.Builders
{
    public class ReaderBuilder: IReaderBuilder

    {
        private string _firstName;
        private string _lastName;
        private string _pesel;

        public IReaderBuilder SetFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public IReaderBuilder SetLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public IReaderBuilder SetPESEL(string pesel)
        {
            _pesel = pesel;
            return this;
        }

        public Reader Build()
        {
            return new Reader(_firstName, _lastName, _pesel);
        }
    }
}
