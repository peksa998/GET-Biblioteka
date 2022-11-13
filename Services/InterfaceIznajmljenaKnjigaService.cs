using GET_Biblioteka.Models;

namespace GET_Biblioteka.Services
{
    public interface InterfaceIznajmljenaKnjigaService
    {
        public bool FindUserRole(string userId);
        void CreateIssuedBook(int reservationId, string userId, int bookId);
        public IznajmljeneKnjigeViewModel GetAllIssuedBooksByUser(string userId);
        public IznajmljeneKnjigeViewModel GetAllIssuedBook();
        void ReturnBook(int issuedBook);
    }
}
