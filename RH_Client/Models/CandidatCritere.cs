namespace RH_Client.Models
{
    public class CandidatCritere : BddObjet
    {
        int id;
        int idCandidat;
        int idCritereOption;

        public CandidatCritere()
        {
            this.init();
        }

        public CandidatCritere(int id, int idCandidat, int idCritereOption)
        {
            Id = id;
            IdCandidat = idCandidat;
            IdCritereOption = idCritereOption;
        }

        public int Id { get => id; set => id = value; }
        public int IdCandidat { get => idCandidat; set => idCandidat = value; }
        public int IdCritereOption { get => idCritereOption; set => idCritereOption = value; }

        public override string tableName()
        {
            return "candidat_critere";
        }
    }
}