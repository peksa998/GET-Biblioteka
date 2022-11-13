namespace GET_Biblioteka.Models
{
    public class Rezultat
    {

        public static Rezultat Success { get; private set; } = new Rezultat() { IsSuccessful = true };
        private Rezultat() { }

        public Rezultat(string error)
        {
            ErrorMessage = error;
        }

        public bool IsSuccessful { get; private set; }

        public string ErrorMessage { get; private set; }

    }
}
