namespace RH_Client.Models
{
    public class CandidatOptionNote : BddObjet
    {
        int id;
        int idcandidat;
        int idcritereoption;
        int nature;
        double note;
        int coeff;

        public CandidatOptionNote() {
            
        }

        public CandidatOptionNote(int id, int idcandidat, int idcritereoption, int nature, double note, int coeff)
        {
            this.Id = id;
            this.Idcandidat = idcandidat;
            this.Idcritereoption = idcritereoption;
            this.Nature = nature;
            this.Note = note;
            this.Coeff = coeff;
        }

        public int Id { get => id; set => id = value; }
        public int Idcandidat { get => idcandidat; set => idcandidat = value; }
        public int Idcritereoption { get => idcritereoption; set => idcritereoption = value; }
        public double Note { get => note; set => note = value; }
        public int Coeff { get => coeff; set => coeff = value; }
        public int Nature { get => nature; set => nature = value; }

        public override String tableName()
        {
            return "vw_candidatoptionnote";
        }
    }
}