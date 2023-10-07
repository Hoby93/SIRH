namespace SIRH.Models
{
    public class Critere : BddObjet
    {
        int id;
        string libelle;
        int nature;

        public Critere() {
            this.init();
        }

        public int Id { get => id; set => id = value; }
        public string Libelle { get => libelle; set => libelle = value; }
        public int Nature { get => nature; set => nature = value; }

        public override String tableName()
        {
            return "critere";
        }

        public Object[] Options() {
            string query_clauses = $"where idcritere = {this.Id}";
            Object[] options = new BddTitre("critere_option").select(query_clauses, null);

            return options;
        }
    }
}