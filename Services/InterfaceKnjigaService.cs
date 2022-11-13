using GET_Biblioteka.Models;

namespace GET_Biblioteka.Services
{
    public interface InterfaceKnjigaService
    {
        public KnjigeViewModel GetAllBooks(string userId);
        public void Create(Knjiga newBook);
        public bool FindUserRole(string userId);
    }
}
