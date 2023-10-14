namespace RH_Client.Models
{
    public class Company : BddObjet
    {
        int id;
        string nom;
        string activite;

        public Company()
        {
            this.init();
        }

        public Company(int id, string nom, string activite)
        {
            Id = id;
            Nom = nom;
            Activite = activite;
        }

        public override String tableName()
        {
            return "entreprise";
        }

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Activite { get => activite; set => activite = value; }
    }
}