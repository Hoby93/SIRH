namespace SIRH.Models
{
    public class Salary : BddObjet
    {
        int id;
        int idEmbauche;
        double valeurBrute;
        double valeurNet;
        DateTime dateDebut;
        DateTime dateFin;

        public Salary()
        {
            this.init();
        }

        public Salary(int id, int idEmbauche, double valeurBrute, double valeurNet, DateTime dateDebut, DateTime dateFin)
        {
            Id = id;
            IdEmbauche = idEmbauche;
            ValeurBrute = valeurBrute;
            ValeurNet = valeurNet;
            DateDebut = dateDebut;
            DateFin = dateFin;
        }

        public int Id { get => id; set => id = value; }
        public int IdEmbauche { get => idEmbauche; set => idEmbauche = value; }
        public double ValeurBrute { get => valeurBrute; set => valeurBrute = value; }
        public double ValeurNet { get => valeurNet; set => valeurNet = value; }
        public DateTime DateDebut { get => dateDebut; set => dateDebut = value; }
        public DateTime DateFin { get => dateFin; set => dateFin = value; }

        public override string tableName()
        {
            return "salaire";
        }
    }
}