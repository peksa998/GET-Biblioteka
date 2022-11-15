using GET_Biblioteka.DAL;
using GET_Biblioteka.Models;
using GET_Biblioteka.SignalHub;
using Microsoft.AspNetCore.SignalR;

namespace GET_Biblioteka.Services
{
    public class IznajmljenaKnjigaService : InterfaceIznajmljenaKnjigaService
    {

        private readonly InterfaceIznajmljenaKnjigaDAL _InterfaceIznajmljenaKnjigaDAL;
        private readonly IHubContext<SignalRHub> _signalRHub;
        public IznajmljenaKnjigaService(InterfaceIznajmljenaKnjigaDAL InterfaceIznajmljenaKnjigaDAL, IHubContext<SignalRHub> signalRHub)
        {
            _InterfaceIznajmljenaKnjigaDAL = InterfaceIznajmljenaKnjigaDAL;
            _signalRHub = signalRHub;
        }

        // brise rezervaciju i salje podatke za kreiranje iznajmljene knjige
        public void CreateIssuedBook(int reservationId, string userId, int bookId)
        {
            _InterfaceIznajmljenaKnjigaDAL.DeleteReservation(reservationId);
            // ovde aktiviramo IssuedBookCreated u signalRhub.js i saljemo mu id rezervacije
            _signalRHub.Clients.All.SendAsync("IssuedBookCreated", reservationId);
            _InterfaceIznajmljenaKnjigaDAL.CreateIssuedBook(userId, bookId);
        }

        // prolaz, 
        public bool FindUserRole(string userId)
        {
            return _InterfaceIznajmljenaKnjigaDAL.FindUserRole(userId);
        }

        // // vraca ViewModel koji pravi na osnovu liste svih iznajmljenih knjiga iz baze, ovo je za admina
        public IznajmljeneKnjigeViewModel GetAllIssuedBook()
        {
            var issuedBooks = _InterfaceIznajmljenaKnjigaDAL.GetAllIssuedBooks();
            IznajmljenaKnjigaViewModel newRes;
            IznajmljeneKnjigeViewModel rvm = new IznajmljeneKnjigeViewModel();
            if (issuedBooks != null)
                foreach (var r in issuedBooks)
                {
                    var title = _InterfaceIznajmljenaKnjigaDAL.GetBookTitleById(r.KnjigaID);
                    var userName = _InterfaceIznajmljenaKnjigaDAL.GetUserNameById(r.UserId);
                    newRes = new IznajmljenaKnjigaViewModel() { DatumVracanja = r.DatumVracanja, NazivKnjige = title, UserName = userName, KnjigaID = r.KnjigaID, UserId = r.UserId, IznajmljenaKnjigaID = r.IznajmljenaKnjigaID };
                    rvm.iznajmljeneKnjige.Add(newRes);
                }
            return rvm;
        }

        // vraca ViewModel koji pravi na osnovu liste svih iznajmljenih knjiga iz baze, ovo je za korisnika
        public IznajmljeneKnjigeViewModel GetAllIssuedBooksByUser(string userId)
        {
            var issuedBooks = _InterfaceIznajmljenaKnjigaDAL.GetAllIssuedBooksByUser(userId);
            IznajmljenaKnjigaViewModel newRes;
            IznajmljeneKnjigeViewModel rvm = new IznajmljeneKnjigeViewModel();
            if (issuedBooks != null)
                foreach (var r in issuedBooks)
                {
                    var title = _InterfaceIznajmljenaKnjigaDAL.GetBookTitleById(r.KnjigaID);
                    var userName = _InterfaceIznajmljenaKnjigaDAL.GetUserNameById(r.UserId);
                    newRes = new IznajmljenaKnjigaViewModel() { DatumVracanja = r.DatumVracanja, NazivKnjige = title, UserName = userName, KnjigaID = r.KnjigaID, UserId = r.UserId, IznajmljenaKnjigaID = r.IznajmljenaKnjigaID };
                    rvm.iznajmljeneKnjige.Add(newRes);
                }
            return rvm;
        }

        // prolaz, prosledjuje id iznajmljivanja
        public void ReturnBook(int issuedBook)
        {
            IznajmljenaKnjiga ReturnedBook = _InterfaceIznajmljenaKnjigaDAL.ReturnBook(issuedBook);
        }
    }
}
