namespace RH_Client.Models
{
    public class BddTitre : BddObjet
    {
        int id;
        string libelle;
        string table;

        public BddTitre()
        {
            this.init();
        }
        public BddTitre(string table)
        {
            this.table = table;
        }

        public int Id { get => id; set => id = value; }
        public string Libelle { get => libelle; set => libelle = value; }

        public override String tableName()
        {
            return this.table;
        }
    }
}