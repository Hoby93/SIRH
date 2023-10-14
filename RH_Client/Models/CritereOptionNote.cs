namespace RH_Client.Models
{
    public class CritereOptionNote : BddObjet
    {
        int id;
        int idbesoincritere;
        int idcritereoption;
        int note;

        public CritereOptionNote() {
            
        }

        public CritereOptionNote(int id, int idbesoincritere, int idcritereoption, int note)
        {
            this.Id = id;
            this.Idbesoincritere = idbesoincritere;
            this.Idcritereoption = idcritereoption;
            this.Note = note;
        }

        public int Id { get => id; set => id = value; }
        public int Idbesoincritere { get => idbesoincritere; set => idbesoincritere = value; }
        public int Idcritereoption { get => idcritereoption; set => idcritereoption = value; }
        public int Note { get => note; set => note = value; }

        public override String tableName()
        {
            return "critere_option_note";
        }
    }
}