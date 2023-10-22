namespace RH_Client.Models
{
    public class Proposition : BddObjet
    {
        int id;
        string libelle;
        int etat;

        public Proposition() {
            this.init();
        }

        public Proposition(int id, string libelle, int etat)
        {
            this.id = id;
            this.libelle = libelle;
            this.etat = etat;
        }

        public int Id { get => id; set => id = value; }
        public string Libelle { get => libelle; set => libelle = value; }
        public int Etat { get => etat; set => etat = value; }

        public override String tableName()
        {
            return "proposition";
        }

        
    }
}