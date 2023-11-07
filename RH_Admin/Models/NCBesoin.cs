using Microsoft.AspNetCore.Routing.Matching;

namespace SIRH.Models
{
    public class NCBesoin
    {
        Candidate candidat;
        NoteCandidat noteCandidat;
        Besoin besoin;
        Poste poste;


        public NCBesoin()
        {
        }

        public NCBesoin(Candidate candidat, NoteCandidat noteCandidat, Besoin besoin, Poste poste)
        {
            this.Candidat = candidat;
            this.NoteCandidat = noteCandidat;
            this.Besoin = besoin;
            this.poste = poste;
        }

        public Candidate Candidat { get => candidat; set => candidat = value; }
        public NoteCandidat NoteCandidat { get => noteCandidat; set => noteCandidat = value; }
        public Besoin Besoin { get => besoin; set => besoin = value; }

        public Poste Poste { get => poste; set => poste = value; }





    }
}