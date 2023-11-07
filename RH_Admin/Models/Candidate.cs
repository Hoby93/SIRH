namespace SIRH.Models
{
    public class Candidate : BddObjet
    {
        int id;
        string nom;
        string prenom;
        string telephone;
        string email;
        string adresse;
        int genre;
        DateTime dateNaissance;

        public Candidate()
        {
        }

        public Candidate(int id, string nom, string prenom, string telephone, string email, string adresse)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Telephone = telephone;
            Email = email;
            Adresse = adresse;
        }

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Email { get => email; set => email = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public int Genre { get => genre; set => genre = value; }
        public DateTime DateNaissance { get => dateNaissance; set => dateNaissance = value; }

        public override string tableName()
        {
            return "candidat";
        }

        public string ShowGenre()
        {
            if (this.Genre == 1)
            {
                return "Homme";
            }
            else if (this.Genre == 0)
            {
                return "Femme";
            }
            return "Non defini";
        }
    }
}