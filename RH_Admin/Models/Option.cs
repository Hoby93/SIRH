namespace SIRH.Models
{
    public class Option : BddObjet
    {
        int id;
        int idCritere;
        string libelle;
        double note;

        public Option()
        {
            this.init();
        }

        public Option(int id, int idCritere, string libelle)
        {
            Id = id;
            IdCritere = idCritere;
            Libelle = libelle;
        }

        public Option(int id, int idCritere, string libelle, double note) : this(id, idCritere, libelle)
        {
            Note = note;
        }

        public int Id { get => id; set => id = value; }
        public int IdCritere { get => idCritere; set => idCritere = value; }
        public string Libelle { get => libelle; set => libelle = value; }
        public double Note { get => note; set => note = value; }

        public override string tableName()
        {
            return "critere_option";
        }
    }
}