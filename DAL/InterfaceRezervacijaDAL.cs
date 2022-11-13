using GET_Biblioteka.Models;

namespace GET_Biblioteka.DAL
{
    public interface InterfaceRezervacijaDAL
    {
        bool FindUserRole(string userId);
        void CreateReservation(Rezervacija newReservation);
        List<Rezervacija> GetAllReservations();
        bool IsBookReservedByUser(int bookId, string userId);
        List<Rezervacija> GetAllReservationsByUser(string userId);
        void DeleteReservation(int reservationId);
        string GetBookTitleById(int id);
        string GetUserNameById(string id);
    }
}
