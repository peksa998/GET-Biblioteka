

using GET_Biblioteka.Models;

namespace GET_Biblioteka.DAL
{
    public interface InterfaceKnjigaDAL
    {
        List<Knjiga> GetAllBooks();
        void CreateBook(Knjiga newBook);
        bool FindUserRole(string userId);
        Knjiga GetBookById(int bookId);
        void UpdateBook(Knjiga book);
        bool IsBookReservedByUser(int bookId, string userId);
        List<Rezervacija> GetAllReservationsByUser(string userId);
        public bool IsReservable(int bookId, string Id);
    }
}
