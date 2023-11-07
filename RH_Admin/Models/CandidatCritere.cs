namespace SIRH.Models
{
    public class CandidatCritere : BddObjet
    {
        int id;
        int idcandidat;
        int idcritereoption;
        string critere;
        int nature;
        string libelle;

        public CandidatCritere() {}

        public int Id { get => id; set => id = value; }
        public int Idcandidat { get => idcandidat; set => idcandidat = value; }
        public int Idcritereoption { get => idcritereoption; set => idcritereoption = value; }
        public string Libelle { get => libelle; set => libelle = value; }
        public string Critere { get => critere; set => critere = value; }
        public int Nature { get => nature; set => nature = value; }

        public override String tableName()
        {
            return "vw_candidatcritere";
        }
    }
}