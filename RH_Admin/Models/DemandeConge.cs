using Npgsql;

namespace SIRH.Models
{
    public class DemandeConge : BddObjet
    {
        int id;
        int idembauche;
        int idtypeconge;
        DateTime debutconge;
        DateTime finconge;
        int etat;

        public DemandeConge()
        {
            this.init();
        }

        public override String tableName()
        {
            return "demandeConge";
        }

        public DemandeConge(int id, int idembauche, int idtypeconge, DateTime debutconge, DateTime finconge, int etat)
        {
            this.Id = id;
            this.Idembauche = idembauche;
            this.Idtypeconge = idtypeconge;
            this.Debutconge = debutconge;
            this.Finconge = finconge;
            this.Etat = etat;
            if (this.Debutconge > this.Finconge)
            {
                throw new Exception("tsy mety debut > fin");
            }
        }


        public int Id { get => id; set => id = value; }
        public int Idembauche { get => idembauche; set => idembauche = value; }
        public int Idtypeconge { get => idtypeconge; set => idtypeconge = value; }
        public DateTime Debutconge { get => debutconge; set => debutconge = value; }
        public DateTime Finconge { get => finconge; set => finconge = value; }
        public int Etat { get => etat; set => etat = value; }

        public AffDemandeConge getAffichage(NpgsqlConnection con)
        {
            bool flag = false;
            if (con == null)
            {
                con = new Connexion().getConnectPostgres();
                flag = true;
            }

            Embauche embauche = (Embauche)new Embauche().select($"WHERE id = {this.Idembauche}", con)[0];
            Candidate candidat = (Candidate)new Candidate().select($"WHERE id = {embauche.IdCandidat}", con)[0];
            TypeConge typeConge = (TypeConge)new TypeConge().select($"WHERE id = {this.Idtypeconge}", con)[0];
            Poste poste = (Poste)new Poste().select($"WHERE id = {embauche.IdPoste}", con)[0];
            DemandeConge demandeConge = this;

            if (flag)
            {
                con.Close();
            }

            return new AffDemandeConge(candidat, typeConge, demandeConge, poste);
        }


        public static AffDemandeConge[] getAllDemandeConge(NpgsqlConnection con)
        {
            bool flag = false;
            if (con == null)
            {
                con = new Connexion().getConnectPostgres();
                flag = true;
            }

            Object[] valiny = new DemandeConge().select($"WHERE etat = 0 and debutConge >= now()", con);
            AffDemandeConge[] farany = new AffDemandeConge[valiny.Length];

            for (int i = 0; i < farany.Length; i++)
            {
                farany[i] = ((DemandeConge)valiny[i]).getAffichage(con);
            }

            if (flag)
            {
                con.Close();
            }

            return farany;
        }

        // raha efa misy conge en cours de tsy afaka mi accepte fa tsy maintsy finir aloha
        public bool estPossible(NpgsqlConnection con)
        {
            bool flag = false;
            if (con == null)
            {
                con = new Connexion().getConnectPostgres();
                flag = true;
            }

            bool valiny = false;

            Object[] val = new ReelConge().select($"WHERE idembauche = {this.Idembauche} AND finConge IS NULL", con);
            if (val.Length == 0)
            {
                valiny = true; // estPossible
            }


            if (flag)
            {
                con.Close();
            }

            return valiny;
        }
    }
}