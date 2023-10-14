using Npgsql;

namespace RH_Client.Models
{
    public class Besoin : BddObjet
    {
        int id;
        int idposte;
        int idtypecontrat;
        string libelle;
        double volumehoraire;
        Poste poste;
        BddTitre contrat;

        double noteadmis;
        DateTime datedebut;
        DateTime datefin;
        int agemin;
        int agemax;

        public Besoin()
        {
            this.init();
        }

        public Besoin(int id, int idposte, int idtypecontrat, string libelle, double volumehoraire)
        {
            this.Id = id;
            this.Idposte = idposte;
            this.Idtypecontrat = idtypecontrat;
            this.Libelle = libelle;
            this.Volumehoraire = volumehoraire;
        }

        public int Id { get => id; set => id = value; }
        public int Idposte { get => idposte; set => idposte = value; }
        public int Idtypecontrat { get => idtypecontrat; set => idtypecontrat = value; }
        public string Libelle { get => libelle; set => libelle = value; }
        public double Volumehoraire { get => volumehoraire; set => volumehoraire = value; }
        public Poste Poste { get => poste; set => poste = value; }
        public BddTitre Contrat { get => contrat; set => contrat = value; }
        public DateTime Datedebut { get => datedebut; set => datedebut = value; }
        public DateTime Datefin { get => datefin; set => datefin = value; }
        public int Agemin { get => agemin; set => agemin = value; }
        public int Agemax { get => agemax; set => agemax = value; }
        public double Noteadmis { get => noteadmis; set => noteadmis = value; }

        public override String tableName()
        {
            return "besoin";
        }

        public void GetJob(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connexion().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                this.Poste = (Poste)new Poste().select("WHERE id = " + this.Idposte, con)[0];
            }
            catch (System.Exception)
            {

                throw;
            }
            finally
            {
                if (isNewConnexion == true)
                {
                    con.Close();
                }
            }
        }

        public void GetContract(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connexion().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                BddTitre bt = new BddTitre("typecontrat");
                this.Contrat = (BddTitre)bt.select("WHERE id = " + this.Idtypecontrat, con)[0];
            }
            catch (System.Exception)
            {

                throw;
            }
            finally
            {
                if (isNewConnexion == true)
                {
                    con.Close();
                }
            }
        }

        public Object[] getCriteres(NpgsqlConnection cnx) {
            Object[] criteres = new BesoinCritere().select($"where idbesoin = {this.id}", cnx);

            return criteres;
        }

        public Boolean IsAdmis(Candidat candidat, NpgsqlConnection cnx) {
            int age = 2023 - candidat.Dtn.Year;
            Console.WriteLine("note"+this.getNoteCriteres(candidat, cnx));
            if(this.getNoteCriteres(candidat, cnx) >= noteadmis && age >= agemin && age <= agemax) {
                return true;
            }

            return false;
        }

        public int incCoeff(CandidatOptionNote option, Boolean isAddition) {
            int ans = 0;
            if(option.Nature == 1) {
                if(!isAddition) {
                    ans = option.Coeff;
                }
                isAddition = true;
            } 
            else {
                isAddition = false;
                ans = option.Coeff;
            }

            return ans;
        }

        public double getNoteCriteres(Candidat candidat, NpgsqlConnection cnx) {
            int idbesoincandidat = new BddObjet().getInteger($"select id from besoin_candidat where idbesoin = {this.Id} and idcandidat = {candidat.Id}", cnx);
            Object[] options = new CandidatOptionNote().select($"where idcandidat = {idbesoincandidat} order by idcritereoption, idbesoincritere", cnx);
            double ans = 0;
            double coeff = 0;
            Boolean isAddition = false;
            int last  = 0;

            foreach(CandidatOptionNote option in options) {
                coeff += incCoeff(option, isAddition);

                if(option.Idcritereoption != last) {
                    ans += option.Note * option.Coeff;
                    last = option.Idcritereoption;
                }
            }

            return  ans;
        }
    }
}