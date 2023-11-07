namespace SIRH.Models
{
    public class Proposal : BddObjet
    {
        int id;
        int idQuestion;
        string libelle;
        int etat;

        public Proposal()
        {
            this.init();
        }

        public Proposal(int id, int idQuestion, string libelle, int etat)
        {
            Id = id;
            IdQuestion = idQuestion;
            Libelle = libelle;
            Etat = etat;
        }

        public int Id { get => id; set => id = value; }
        public int IdQuestion { get => idQuestion; set => idQuestion = value; }
        public string Libelle { get => libelle; set => libelle = value; }
        public int Etat { get => etat; set => etat = value; }

        public override string tableName()
        {
            return "proposition";
        }
    }
}