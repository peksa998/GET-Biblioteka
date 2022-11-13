using GET_Biblioteka.Data;
using GET_Biblioteka.Models;

namespace GET_Biblioteka.DAL
{
    public class KnjigaDAL : InterfaceKnjigaDAL
    {
        private ApplicationDbContext _context;
        public KnjigaDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateBook(Knjiga newBook)
        {
            _context.Knjige.Add(newBook);
            _context.SaveChanges();
        }

        public bool FindUserRole(string userId)
        {
            var role = _context.UserRoles.Where(x => x.UserId == userId).FirstOrDefault();
            if (role.RoleId == "1") return true;
            else return false;
        }

        public List<Knjiga> GetAllBooks()
        {
            return _context.Knjige.ToList();
        }

        public List<Rezervacija> GetAllReservationsByUser(string userId)
        {
            return _context.Rezervacije.Where(r => r.UserId == userId).ToList();
        }

        public Knjiga GetBookById(int bookId)
        {
            return _context.Knjige.FirstOrDefault(b => b.KnjigaID == bookId);
        }

        public bool IsBookReservedByUser(int bookId, string userId)
        {
            return _context.Rezervacije.FirstOrDefault(r => r.UserId == userId && r.KnjigaID == bookId) != null;
        }

        public bool IsBookIssuedToUser(int bookId, string userId)
        {
            return _context.IznajmljeneKnjige.FirstOrDefault(r => r.UserId == userId && r.KnjigaID == bookId) != null;
        }

        public bool IsReservable(int bookId, string Id)
        {
            List<Rezervacija> list = GetAllReservationsByUser(Id);
            Knjiga b = GetBookById(bookId);
            if (IsBookReservedByUser(bookId, Id) == true) return false;
            else if (list.Count > 4) return false;
            else if (b.Kolicina < 1) return false;
            else if (IsBookIssuedToUser(bookId, Id) == true) return false;
            return true;
        }

        public void UpdateBook(Knjiga book)
        {
            var dbBook = _context.Knjige.FirstOrDefault(b => b.KnjigaID == book.KnjigaID);
            if (dbBook != null)
            {
                dbBook.Pisac = book.Pisac;
                dbBook.Kolicina = book.Kolicina;
                dbBook.Naziv = book.Naziv;
                _context.SaveChanges();
            }
        }
    }
}
