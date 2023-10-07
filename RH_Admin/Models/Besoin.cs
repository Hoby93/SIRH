namespace SIRH.Models
{
    public class Besoin : BddObjet
    {
        int id;
        int idposte;
        int idtypecontrat;
        string libelle;
        double volumehoraire;

        public Besoin() {
            this.init();
        }

        public Besoin(int id, int idposte, int idtypecontrat, string libelle, double volumehoraire)
        {
            this.Id = id;
            this.Idposte = idposte;
            this.Idtypecontrat = idtypecontrat;
            this.Libelle = libelle;
            this.Volumehoraire = volumehoraire;
        }

        public int Id { get => id; set => id = value; }
        public int Idposte { get => idposte; set => idposte = value; }
        public int Idtypecontrat { get => idtypecontrat; set => idtypecontrat = value; }
        public string Libelle { get => libelle; set => libelle = value; }
        public double Volumehoraire { get => volumehoraire; set => volumehoraire = value; }

        public override String tableName()
        {
            return "besoin";
        }
    }
}