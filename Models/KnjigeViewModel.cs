namespace GET_Biblioteka.Models
{
    public class KnjigeViewModel
    {
        public List<KnjigaViewModel> Knjige { get; set; } = new List<KnjigaViewModel>();
        public string Error { get; set; }
        public bool IsError => !string.IsNullOrEmpty(Error);
    }
    public class KnjigaViewModel
    {

        public int KnjigaID { get; set; }
        public string Naziv { get; set; }
        public string Pisac { get; set; }
        public int Kolicina { get; set; }
        public bool Dostupna { get; set; }
    }
}
