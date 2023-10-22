namespace RH_Client.Models
{
    public class VwCandidatReponse : BddObjet
    {
        int id;
        int idbesoincandidat;
        int idproposition;
        int idquestion;

        public VwCandidatReponse() {}

        public VwCandidatReponse(int id, int idbesoincandidat, int idproposition,int idquestion)
        {
            this.id = id;
            this.idbesoincandidat = idbesoincandidat;
            this.idproposition = idproposition;
            this.idquestion = idquestion;
        }

        public int Id { get => id; set => id = value; }
        public int Idbesoincandidat { get => idbesoincandidat; set => idbesoincandidat = value; }
        public int Idproposition { get => idproposition; set => idproposition = value; }
        public int Idquestion { get => idquestion; set => idquestion = value; }

        public override String tableName()
        {
            return "vw_candidat_reponse";
        }
    }
}