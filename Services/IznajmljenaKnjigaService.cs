using GET_Biblioteka.DAL;
using GET_Biblioteka.Models;

namespace GET_Biblioteka.Services
{
    public class IznajmljenaKnjigaService : InterfaceIznajmljenaKnjigaService
    {

        private readonly InterfaceIznajmljenaKnjigaDAL _InterfaceIznajmljenaKnjigaDAL;
        public IznajmljenaKnjigaService(InterfaceIznajmljenaKnjigaDAL InterfaceIznajmljenaKnjigaDAL)
        {
            _InterfaceIznajmljenaKnjigaDAL = InterfaceIznajmljenaKnjigaDAL;
        }


        public void CreateIssuedBook(int reservationId, string userId, int bookId)
        {
            _InterfaceIznajmljenaKnjigaDAL.DeleteReservation(reservationId);
            _InterfaceIznajmljenaKnjigaDAL.CreateIssuedBook(userId, bookId);
        }

        public bool FindUserRole(string userId)
        {
            return _InterfaceIznajmljenaKnjigaDAL.FindUserRole(userId);
        }

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

        public void ReturnBook(int issuedBook)
        {
            IznajmljenaKnjiga ReturnedBook = _InterfaceIznajmljenaKnjigaDAL.ReturnBook(issuedBook);
        }
    }
}
