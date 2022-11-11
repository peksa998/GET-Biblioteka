using System.ComponentModel.DataAnnotations;

namespace GET_Biblioteka.Models
{
    public class Knjiga
    {

        [Key]
        public int KnjigaID { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public string Pisac { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Broj knjiga mora da bude veci od 0.")]
        public int Kolicina { get; set; }
    }
}
