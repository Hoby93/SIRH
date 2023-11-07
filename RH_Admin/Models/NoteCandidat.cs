using Microsoft.AspNetCore.Routing.Matching;
using Npgsql;

namespace SIRH.Models
{
    public class NoteCandidat : BddObjet
    {
        int id;
        int idcandidat;
        int idbesoin;
        double note;

        public NoteCandidat()
        {
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

        public static List<NoteCandidat> getAllNoteCandidat(NpgsqlConnection c, double notemin, int idbesoin)
        {
            bool flag = false;
            if (c == null)
            {
                c = new Connexion().getConnectPostgres();
                flag = true;
            }

            List<NoteCandidat> val = new List<NoteCandidat>();

            Object[] obj = new NoteCandidat().select($"WHERE note >= {notemin} AND idbesoin = {idbesoin} ORDER BY note DESC", c);

            for (int i = 0; i < obj.Length; i++)
            {
                NoteCandidat nc = (NoteCandidat)obj[i];
                if (nc.estDejaRecrute(c) == false)
                {
                    val.Add(nc);
                }
            }

            if (flag)
            {
                c.Close();
            }

            return val;
        }

        public NCBesoin getCandidatBesoin(NpgsqlConnection c)
        {
            bool flag = false;
            if (c == null)
            {
                c = new Connexion().getConnectPostgres();
                flag = true;
            }

            Candidate candidat = (Candidate)new Candidate().select($"WHERE id = {this.Idcandidat}", c)[0];

            Besoin besoin = (Besoin)new Besoin().select($"WHERE id = {this.Idbesoin}", c)[0];

            Poste poste = (Poste)new Poste().select($"WHERE id = {besoin.Idposte}", c)[0];

            NCBesoin val = new NCBesoin(candidat, this, besoin, poste);

            if (flag)
            {
                c.Close();
            }

            return val;
        }

        public bool estDejaRecrute(NpgsqlConnection c)
        {
            bool val = false;
            bool flag = false;
            if (c == null)
            {
                c = new Connexion().getConnectPostgres();
                flag = true;
            }

            Object[] embauche = new Embauche().select($"WHERE idcandidat = {this.Idcandidat}", c);

            if (embauche.Length > 0)
            {
                val = true; // il est deja embauche
            }

            if (flag)
            {
                c.Close();
            }

            return val;
        }

        public List<NCBesoin> getAllCandidatBesoin(NpgsqlConnection c, double notemin, int idbesoin)
        {
            bool flag = false;
            if (c == null)
            {
                c = new Connexion().getConnectPostgres();
                flag = true;
            }

            List<NoteCandidat> nc = NoteCandidat.getAllNoteCandidat(c, notemin, idbesoin);
            List<NCBesoin> val = new List<NCBesoin>();
            for (int i = 0; i < nc.Count; i++)
            {
                val.Add(nc[i].getCandidatBesoin(c));
            }

            if (flag)
            {
                c.Close();
            }

            return val;
        }





    }
}