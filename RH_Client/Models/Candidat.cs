namespace RH_Client.Models
{
    public class Candidat : BddObjet
    {
        int id;
        string nom;
        string prenom;
        DateTime dtn;
        string telephone;
        string email;
        string adresse;

        public Candidat() {
            this.init();
        }

         public override String tableName()
        {
            return "candidat";
        }

        public Candidat(int id, string nom, string prenom, DateTime dtn, string telephone, string email, string adresse)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.dtn = dtn;
            this.telephone = telephone;
            this.email = email;
            this.adresse = adresse;
        }


        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public DateTime Dtn { get => dtn; set => dtn = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Email { get => email; set => email = value; }
        public string Adresse { get => adresse; set => adresse = value; }
    }
}