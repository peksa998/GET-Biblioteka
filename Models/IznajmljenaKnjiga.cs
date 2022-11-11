using System.ComponentModel.DataAnnotations;

namespace GET_Biblioteka.Models
{
    public class IznajmljenaKnjiga
    {
        [Key]
        public int IznajmljenaKnjigaID { get; set; }
        [Required]
        public int KnjigaID { get; set; }
        [Required]
        public string UserId { get; set; }
        public DateTime DatumVracanja { get; set; }
    }
}
