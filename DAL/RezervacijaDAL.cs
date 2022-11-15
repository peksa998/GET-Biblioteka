using GET_Biblioteka.Data;
using GET_Biblioteka.Models;

namespace GET_Biblioteka.DAL
{
    public class RezervacijaDAL : InterfaceRezervacijaDAL
    {

        private ApplicationDbContext _context;
        public RezervacijaDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        // kreiranje nove rezervacije
        public void CreateReservation(Rezervacija newReservation)
        {
            _context.Rezervacije.Add(newReservation);
            _context.SaveChanges();
        }

        // brisanje rezervacije na osnovi id
        public void DeleteReservation(int reservationId)
        {
            var res = _context.Rezervacije.Where(r => r.RezervacijaID == reservationId).FirstOrDefault();
            if (res != null)
            {
                var book = _context.Knjige.Where(r => r.KnjigaID == res.KnjigaID).FirstOrDefault();
                if (book != null)
                    ++book.Kolicina;
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

        // vraca listu svih rezervacija
        public List<Rezervacija> GetAllReservations()
        {
            return _context.Rezervacije.ToList();
        }

        // vraca listu rezervacija za odredjenog korisnika
        public List<Rezervacija> GetAllReservationsByUser(string userId)
        {
            return _context.Rezervacije.Where(r => r.UserId == userId).ToList();
        }

        public string GetBookTitleById(int id)
        {
            return _context.Knjige.Where(r => r.KnjigaID == id).FirstOrDefault().Naziv.ToString();
        }

        public string GetUserNameById(string id)
        {
            return _context.Users.Where(r => r.Id == id).FirstOrDefault().UserName.ToString();
        }

        public bool IsBookReservedByUser(int bookId, string userId)
        {
            return _context.Rezervacije.FirstOrDefault(r => r.UserId == userId && r.KnjigaID == bookId) != null;
        }
    }
}
