using GET_Biblioteka.Models;

namespace GET_Biblioteka.DAL
{
    public interface InterfaceIznajmljenaKnjigaDAL
    {

        List<IznajmljenaKnjiga> GetAllIssuedBooks();
        bool FindUserRole(string userId);
        public void DeleteReservation(int reservationId);
        void CreateIssuedBook(string userId, int bookId);
        string GetBookTitleById(int bookId);
        string GetUserNameById(string userId);
        List<IznajmljenaKnjiga> GetAllIssuedBooksByUser(string userId);
        IznajmljenaKnjiga ReturnBook(int issuedBook);
    }
}
