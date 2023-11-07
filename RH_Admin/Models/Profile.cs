namespace SIRH.Models
{
    public class Profile
    {
        Critere critere;
        Option option;

        public Profile()
        {
        }

        public Profile(Critere critere, Option option)
        {
            Critere = critere;
            Option = option;
        }

        public Critere Critere { get => critere; set => critere = value; }
        public Option Option { get => option; set => option = value; }
    }
}