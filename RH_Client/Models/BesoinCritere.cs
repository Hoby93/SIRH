namespace clientGRH.Models
{
    public class BesoinCritere : BddObjet
    {
        int id;
        int idbesoin;
        int idcritere;
        int coeff;

        public BesoinCritere() {
            
        }

        public BesoinCritere(int id, int idbesoin, int idcritere, int coeff)
        {
            this.Id = id;
            this.Idbesoin = idbesoin;
            this.Idcritere = idcritere;
            this.Coeff = coeff;
        }

        public int Id { get => id; set => id = value; }
        public int Idbesoin { get => idbesoin; set => idbesoin = value; }
        public int Idcritere { get => idcritere; set => idcritere = value; }
        public int Coeff { get => coeff; set => coeff = value; }

        public override String tableName()
        {
            return "besoin_critere";
        }

        
    }
}