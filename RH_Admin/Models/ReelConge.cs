using Npgsql;

namespace SIRH.Models
{
    public class ReelConge : BddObjet
    {
        int id;
        int idembauche;
        int idtypeconge;
        DateTime debutconge;
        DateTime? finconge;

        public ReelConge()
        {
            this.init();
        }

        public override String tableName()
        {
            return "reelConge";
        }

        public ReelConge(int id, int idembauche, int idtypeconge, DateTime debutconge, DateTime? finconge)
        {
            this.Id = id;
            this.Idembauche = idembauche;
            this.Idtypeconge = idtypeconge;
            this.Debutconge = debutconge;
            this.Finconge = finconge;
            if (this.Debutconge > this.Finconge)
            {
                throw new Exception("tsy mety debut > fin");
            }
        }

        public int Id { get => id; set => id = value; }
        public int Idembauche { get => idembauche; set => idembauche = value; }
        public int Idtypeconge { get => idtypeconge; set => idtypeconge = value; }
        public DateTime Debutconge { get => debutconge; set => debutconge = value; }
        public DateTime? Finconge { get => finconge; set => finconge = value; }

        public AffReelConge getAffichage(NpgsqlConnection con)
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
            ReelConge reelConge = this;

            if (flag)
            {
                con.Close();
            }

            return new AffReelConge(candidat, typeConge, reelConge, poste);
        }


        public static AffReelConge[] getAllCongeEnCours(NpgsqlConnection con)
        {
            bool flag = false;
            if (con == null)
            {
                con = new Connexion().getConnectPostgres();
                flag = true;
            }

            Object[] valiny = new ReelConge().select($"WHERE finConge IS NULL", con);
            AffReelConge[] farany = new AffReelConge[valiny.Length];

            for (int i = 0; i < farany.Length; i++)
            {
                farany[i] = ((ReelConge)valiny[i]).getAffichage(con);
            }

            if (flag)
            {
                con.Close();
            }

            return farany;
        }


    }
}