namespace RH_Client.Models
{
    public class CandidatReponse : BddObjet
    {
        int id;
        int idbesoincandidat;
        int idproposition;

        public CandidatReponse() {}

        public CandidatReponse(int id, int idbesoincandidat, int idproposition)
        {
            this.id = id;
            this.idbesoincandidat = idbesoincandidat;
            this.idproposition = idproposition;
        }

        public int Id { get => id; set => id = value; }
        public int Idbesoincandidat { get => idbesoincandidat; set => idbesoincandidat = value; }
        public int Idproposition { get => idproposition; set => idproposition = value; }

        public override String tableName()
        {
            return "candidat_reponse";
        }
    }
}