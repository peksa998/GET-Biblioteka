using GET_Biblioteka.Models;

namespace GET_Biblioteka.Services
{
    public interface InterfaceRezervacijaService
    {
        public RezervacijeViewModel GetAllReservations();
        public bool FindUserRole(string userId);
        Rezultat Create(int bookId, string userId);
        void DeleteReservation(int reservationId);
        RezervacijeViewModel GetAllReservationsByUser(string userId);
    }
}
