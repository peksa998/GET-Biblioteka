using GET_Biblioteka.DAL;
using GET_Biblioteka.Models;

namespace GET_Biblioteka.Services
{
    public class RezervacijaService : InterfaceRezervacijaService
    {

        private readonly InterfaceRezervacijaDAL _InterfaceRezervacijaDAL;
        private readonly InterfaceKnjigaDAL _InterfaceKnjigaDAL;

        public RezervacijaService(InterfaceRezervacijaDAL InterfaceRezervacijaDAL, InterfaceKnjigaDAL InterfaceKnjigaDAL)
        {
            _InterfaceRezervacijaDAL = InterfaceRezervacijaDAL;
            _InterfaceKnjigaDAL = InterfaceKnjigaDAL;
        }

        public Rezultat Create(int bookId, string userId)
        {
            if (_InterfaceRezervacijaDAL.IsBookReservedByUser(bookId, userId))
            {
                throw new Exception("Vec ste rezervisali ovu knjigu!");
            }

            var book = _InterfaceKnjigaDAL.GetBookById(bookId);
            if (book == null)
            {
                throw new Exception("Knjiga ne postoji.");
            }

            if (book.Kolicina < 1)
            {
                throw new Exception("Knjiga nije na stanju");
            }


            var reservation = new Rezervacija() { KnjigaID = bookId, UserId = userId };
            _InterfaceRezervacijaDAL.CreateReservation(reservation);
            --book.Kolicina;
            _InterfaceKnjigaDAL.UpdateBook(book);
            Console.WriteLine(reservation.RezervacijaID);
            return Rezultat.Success;
        }

        public void DeleteReservation(int reservationId)
        {
            _InterfaceRezervacijaDAL.DeleteReservation(reservationId);
        }

        public bool FindUserRole(string userId)
        {
            return _InterfaceRezervacijaDAL.FindUserRole(userId);
        }

        public RezervacijeViewModel GetAllReservations()
        {
            var reservations = _InterfaceRezervacijaDAL.GetAllReservations();
            RezervacijaViewModel newRes;
            RezervacijeViewModel rvm = new RezervacijeViewModel();
            if (reservations != null)
                foreach (var r in reservations)
                {
                    var title = _InterfaceRezervacijaDAL.GetBookTitleById(r.KnjigaID);
                    var userName = _InterfaceRezervacijaDAL.GetUserNameById(r.UserId);
                    newRes = new RezervacijaViewModel() { RezervacijaID = r.RezervacijaID, NazivKnjige = title, UserName = userName, KnjigaID = r.KnjigaID, UserId = r.UserId };
                    rvm.Rezervacije.Add(newRes);
                }
            return rvm;
        }

        public RezervacijeViewModel GetAllReservationsByUser(string userId)
        {
            var reservations = _InterfaceRezervacijaDAL.GetAllReservationsByUser(userId);
            RezervacijaViewModel newRes;
            RezervacijeViewModel rvm = new RezervacijeViewModel();
            if (reservations != null)
                foreach (var r in reservations)
                {
                    var title = _InterfaceRezervacijaDAL.GetBookTitleById(r.KnjigaID);
                    var userName = _InterfaceRezervacijaDAL.GetUserNameById(r.UserId);
                    newRes = new RezervacijaViewModel() { RezervacijaID = r.RezervacijaID, NazivKnjige = title, UserName = userName, KnjigaID = r.KnjigaID, UserId = r.UserId };
                    rvm.Rezervacije.Add(newRes);
                }
            return rvm;
        }
    }
}
