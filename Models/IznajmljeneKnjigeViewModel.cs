namespace GET_Biblioteka.Models
{
    public class IznajmljeneKnjigeViewModel
    {
        public List<IznajmljenaKnjigaViewModel> iznajmljeneKnjige = new List<IznajmljenaKnjigaViewModel>();
    }
    public class IznajmljenaKnjigaViewModel
    {
        public int IznajmljenaKnjigaID { get; set; }
        public int KnjigaID { get; set; }
        public string UserId { get; set; }
        public DateTime DatumVracanja { get; set; }
        public string UserName { get; set; }
        public string NazivKnjige { get; set; }
    }

}
