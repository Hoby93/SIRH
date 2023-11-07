using Microsoft.AspNetCore.Routing.Matching;
using Npgsql;

namespace RH_Client.Models
{
    public class NoteCandidat : BddObjet
    {
        int id;
        int idcandidat;
        int idbesoin;
        double note;

        public NoteCandidat() {
            this.init();
        }

        public NoteCandidat(int id, int idcandidat, int idbesoin, double note)
        {
            this.Id = id;
            this.Idcandidat = idcandidat;
            this.Idbesoin = idbesoin;
            this.Note = note;
        }

        public int Id { get => id; set => id = value; }
        public int Idcandidat { get => idcandidat; set => idcandidat = value; }
        public int Idbesoin { get => idbesoin; set => idbesoin = value; }
        
        public double Note { get => note; set => note = value; }

        public override String tableName()
        {
            return "noteCandidat";
        }

    }
}