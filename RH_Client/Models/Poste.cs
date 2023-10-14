namespace RH_Client.Models
{
    public class Poste : BddObjet
    {
        int id;
        int idservice;
        string libelle;
        string description;


        public Poste() {
            this.init();
        }
        
        public Poste(int id, int idservice, string libelle, string description)
        {
            this.id = id;
            this.idservice = idservice;
            this.libelle = libelle;
            this.description = description;
        }

        public int Id { get => id; set => id = value; }
        public int Idservice { get => idservice; set => idservice = value; }
        public string Libelle { get => libelle; set => libelle = value; }
        public string Description { get => description; set => description = value; }

        public override String tableName()
        {
            return "poste";
        }
    }
}