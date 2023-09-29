namespace Test.Models
{
    public class Employe : BddObjet
    {
        int id;
        string nom;
        string prenom;
        double ip;
        DateTime datenaissance;
        
        public Employe() { 
            this.init();
        }

        public Employe(int id, string nom, string prenom, double ip, DateTime datenaissance)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Ip = ip;
            Datenaissance = datenaissance;
        }

        public Employe(string nom, string prenom)
        {
            this.init();
            this.nom = nom;
            this.prenom = prenom;
        }

        public override String tableName()
        {
            return "employe";
        }

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public double Ip { get => ip; set => ip = value; }
        public DateTime Datenaissance { get => datenaissance; set => datenaissance = value; }
    }
}