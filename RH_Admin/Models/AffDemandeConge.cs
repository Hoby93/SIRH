using Npgsql;

namespace SIRH.Models
{

    // pour l'affichage
    public class AffDemandeConge
    {
        Candidate candidat; // embauche
        TypeConge typeConge;
        DemandeConge demandeConge;
        Poste poste;

        public AffDemandeConge()
        {

        }
        public AffDemandeConge(Candidate c, TypeConge tc, DemandeConge dc, Poste p)
        {
            this.Candidat = c;
            this.TypeConge = tc;
            this.DemandeConge = dc;
            this.Poste = p;
        }

        public Candidate Candidat { get => candidat; set => candidat = value; }
        public TypeConge TypeConge { get => typeConge; set => typeConge = value; }
        public DemandeConge DemandeConge { get => demandeConge; set => demandeConge = value; }
        public Poste Poste { get => poste; set => poste = value; }
    }

}