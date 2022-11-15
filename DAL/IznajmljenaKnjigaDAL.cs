
using GET_Biblioteka.Data;
using GET_Biblioteka.Models;

namespace GET_Biblioteka.DAL
{
    public class IznajmljenaKnjigaDAL : InterfaceIznajmljenaKnjigaDAL
    {

        private ApplicationDbContext _context;
        public IznajmljenaKnjigaDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateIssuedBook(IznajmljenaKnjiga newIssuedBook)
        {
            _context.IznajmljeneKnjige.Add(newIssuedBook);
            _context.SaveChanges();
        }

        // kreiranje iznajmljene knjige
        public void CreateIssuedBook(string userId, int bookId)
        {
            IznajmljenaKnjiga ib = new IznajmljenaKnjiga() { KnjigaID = bookId, UserId = userId, DatumVracanja = DateTime.Today.AddMonths(1) };
            _context.IznajmljeneKnjige.Add(ib);
            _context.SaveChanges();
        }

        // brisanje rezeervacije
        public void DeleteReservation(int reservationId)
        {
            var res = _context.Rezervacije.Where(r => r.RezervacijaID == reservationId).FirstOrDefault();
            if (res != null)
            {
                _context.Remove(res);
                _context.SaveChanges();
            }
        }

        // vraca false za korisnika, true za admina
        public bool FindUserRole(string userId)
        {
            var role = _context.UserRoles.Where(x => x.UserId == userId).FirstOrDefault();
            if (role.RoleId == "1") return true;
            else return false;
        }

        public List<IznajmljenaKnjiga> GetAllIssuedBooks()
        {
            return _context.IznajmljeneKnjige.ToList();
        }

        public List<IznajmljenaKnjiga> GetAllIssuedBooksByUser(string userId)
        {
            return _context.IznajmljeneKnjige.Where(r => r.UserId == userId).ToList();
        }

        public string GetBookTitleById(int bookId)
        {
            return _context.Knjige.Where(r => r.KnjigaID == bookId).FirstOrDefault().Naziv.ToString();
        }

        public string GetUserNameById(string userId)
        {
            return _context.Users.Where(r => r.Id == userId).FirstOrDefault().UserName.ToString();
        }

        // vracanje knjige
        // povecava kolicinu knjige za 1, pa brise iznajmljenu knjigu
        public IznajmljenaKnjiga ReturnBook(int BookIssueId)
        {
            var res = _context.IznajmljeneKnjige.Where(r => r.IznajmljenaKnjigaID == BookIssueId).FirstOrDefault();
            if (res != null)
            {
                var book = _context.Knjige.Where(r => r.KnjigaID == res.KnjigaID).FirstOrDefault();
                if (book != null)
                    ++book.Kolicina;
                _context.Remove(res);
                _context.SaveChanges();
            }
            return res;
        }
    }
}
