namespace GET_Biblioteka.Models
{
    public class RezervacijeViewModel
    {
        public List<RezervacijaViewModel> Rezervacije = new List<RezervacijaViewModel>();
    }

    public class RezervacijaViewModel
    {
        public int RezervacijaID { get; set; }
        public string UserName { get; set; }
        public string NazivKnjige { get; set; }
        public string UserId { get; set; }
        public int KnjigaID { get; set; }
    }
}
