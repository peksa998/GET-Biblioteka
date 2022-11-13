using GET_Biblioteka.DAL;
using GET_Biblioteka.Models;

namespace GET_Biblioteka.Services
{
    public class KnjigaService : InterfaceKnjigaService
    {
        private readonly InterfaceKnjigaDAL _InterfaceKnjigaDAL;
        public KnjigaService(InterfaceKnjigaDAL InterfaceKnjigaDAL)
        {
            _InterfaceKnjigaDAL = InterfaceKnjigaDAL;
        }

        public void Create(Knjiga newBook)
        {
            _InterfaceKnjigaDAL.CreateBook(newBook);
        }

        public bool FindUserRole(string userId)
        {
            return _InterfaceKnjigaDAL.FindUserRole(userId);
        }

        public KnjigeViewModel GetAllBooks(string userId)
        {
            var books = _InterfaceKnjigaDAL.GetAllBooks();
            bool res;
            KnjigaViewModel newBook;
            KnjigeViewModel bvm = new KnjigeViewModel();
            foreach (var b in books)
            {
                res = _InterfaceKnjigaDAL.IsReservable(b.KnjigaID, userId);
                newBook = new KnjigaViewModel() { Pisac = b.Pisac, KnjigaID = b.KnjigaID, Kolicina = b.Kolicina, Naziv = b.Naziv, Dostupna = res };
                bvm.Knjige.Add(newBook);
            }
            return bvm;
        }
    }
}
