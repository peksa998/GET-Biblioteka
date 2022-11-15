using GET_Biblioteka.DAL;
using GET_Biblioteka.Models;
using GET_Biblioteka.SignalHub;
using Microsoft.AspNetCore.SignalR;

namespace GET_Biblioteka.Services
{
    public class RezervacijaService : InterfaceRezervacijaService
    {

        private readonly InterfaceRezervacijaDAL _InterfaceRezervacijaDAL;
        private readonly InterfaceKnjigaDAL _InterfaceKnjigaDAL;
        private readonly IHubContext<SignalRHub> _signalRHub;

        public RezervacijaService(InterfaceRezervacijaDAL InterfaceRezervacijaDAL, InterfaceKnjigaDAL InterfaceKnjigaDAL, IHubContext<SignalRHub> signalRHub)
        {
            _InterfaceRezervacijaDAL = InterfaceRezervacijaDAL;
            _InterfaceKnjigaDAL = InterfaceKnjigaDAL;
            _signalRHub = signalRHub;
        }

        // kreira rezervaciju, salje je dalje u bazu i ako je sve ok vraca true preko Rezultat
        public Rezultat Create(int bookId, string userId)
        {
            // ovo nije potrebno, clickable resio 
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

            // pravi rezervaciju, upisuje je u bazu; radi se update knjige za kolicinu
            var reservation = new Rezervacija() { KnjigaID = bookId, UserId = userId };
            _InterfaceRezervacijaDAL.CreateReservation(reservation);
            --book.Kolicina;
            _InterfaceKnjigaDAL.UpdateBook(book);

            // ovde aktiviramo ReservationCreated u signalRhub.js i saljemo mu ove podatke
            _signalRHub.Clients.All.SendAsync("ReservationCreated", reservation.RezervacijaID, reservation.KnjigaID, reservation.UserId, _InterfaceRezervacijaDAL.GetBookTitleById(reservation.KnjigaID), _InterfaceRezervacijaDAL.GetUserNameById(reservation.UserId));
            Console.WriteLine(reservation.RezervacijaID);
            return Rezultat.Success;
        }

        // prolaz
        public void DeleteReservation(int reservationId)
        {
            _InterfaceRezervacijaDAL.DeleteReservation(reservationId);  
        }

        // prolaz, vraca false za korisnika, true za admina
        public bool FindUserRole(string userId)
        {
            return _InterfaceRezervacijaDAL.FindUserRole(userId);
        }

        // vraca ViewModel koji pravi na osnovu liste svih rezervacija iz baze, ovo je za admina
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

        // vraca ViewModel koji pravi na osnovu liste svih rezervacija iz baze, ovo je za korisnika
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
