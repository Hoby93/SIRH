using Npgsql;

namespace SIRH.Models
{

    // pour l'affichage
    public class AffReelConge
    {
        Candidate candidat; // embauche
        TypeConge typeConge;
        ReelConge reelConge;
        Poste poste;

        public AffReelConge()
        {

        }
        public AffReelConge(Candidate c, TypeConge tc, ReelConge rc, Poste p)
        {
            this.Candidat = c;
            this.TypeConge = tc;
            this.ReelConge = rc;
            this.Poste = p;
        }

        public Candidate Candidat { get => candidat; set => candidat = value; }
        public TypeConge TypeConge { get => typeConge; set => typeConge = value; }
        public ReelConge ReelConge { get => reelConge; set => reelConge = value; }
        public Poste Poste { get => poste; set => poste = value; }
    }

}