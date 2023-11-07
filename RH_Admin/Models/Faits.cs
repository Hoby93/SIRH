namespace SIRH.Models
{
    public class Faits : BddObjet
    {
        int id;
        int idembauche;
        int fait;
        DateTime datefait;


        public Faits() {
            this.init();
        }

        public Faits(int id, int idembauche, int fait, DateTime datefait)
        {
            this.id = id;
            this.idembauche = idembauche;
            this.fait = fait;
            this.datefait = datefait;
        }

        public int Id { get => id; set => id = value; }
        public int Idembauche { get => idembauche; set => idembauche = value; }
        public int Fait { get => fait; set => fait = value; }
        public DateTime Datefait { get => datefait; set => datefait = value; }

        public override String tableName()
        {
            return "historique_fait";
        }
    }
}