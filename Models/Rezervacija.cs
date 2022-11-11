using System.ComponentModel.DataAnnotations;

namespace GET_Biblioteka.Models
{
    public class Rezervacija
    {

        [Key]
        public int RezervacijaID { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int KnjigaID { get; set; }
    }
}
