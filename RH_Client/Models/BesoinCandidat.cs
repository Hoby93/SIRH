namespace RH_Client.Models
{
    public class BesoinCandidat : BddObjet
    {
        int id;
        int idCandidat;
        int idBesoin;
        DateTime dateInscription;

        public BesoinCandidat()
        {
            this.init();
        }

        public BesoinCandidat(int id, int idCandidat, int idBesoin, DateTime dateInscription)
        {
            Id = id;
            IdCandidat = idCandidat;
            IdBesoin = idBesoin;
            DateInscription = dateInscription;
        }

        public int Id { get => id; set => id = value; }
        public int IdCandidat { get => idCandidat; set => idCandidat = value; }
        public int IdBesoin { get => idBesoin; set => idBesoin = value; }
        public DateTime DateInscription { get => dateInscription; set => dateInscription = value; }

        public override string tableName()
        {
            return "besoin_candidat";
        }
    }
}