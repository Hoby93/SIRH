using Npgsql;

namespace SIRH.Models
{
    public class TrialContract : BddObjet
    {
        int id;
        int idEmbauche;
        double tempsTravail;
        DateTime dateDebutContrat;
        double dureeEssai;
        double joursTravailles;

        public TrialContract()
        {
            this.init();
        }

        public TrialContract(int id, int idEmbauche, double tempsTravail, DateTime dateDebutContrat, double dureeEssai, double joursTravailles)
        {
            Id = id;
            IdEmbauche = idEmbauche;
            TempsTravail = tempsTravail;
            DateDebutContrat = dateDebutContrat;
            DureeEssai = dureeEssai;
            JoursTravailles = joursTravailles;
        }

        public int Id { get => id; set => id = value; }
        public int IdEmbauche { get => idEmbauche; set => idEmbauche = value; }
        public double TempsTravail { get => tempsTravail; set => tempsTravail = value; }
        public DateTime DateDebutContrat { get => dateDebutContrat; set => dateDebutContrat = value; }
        public double DureeEssai { get => dureeEssai; set => dureeEssai = value; }
        public double JoursTravailles { get => joursTravailles; set => joursTravailles = value; }

        public override string tableName()
        {
            return "contratEssai";
        }

        public void Create(NpgsqlConnection con, int[] avantages)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                ContratEssaiAvantage cea = new ContratEssaiAvantage();
                this.insert(con);
                cea.IdContratEssai = this.getInteger("SELECT MAX(id) FROM contratEssai", con);
                for (int i = 0; i < avantages.Length; i++)
                {
                    cea.IdAvantage = avantages[i];
                    cea.insert(con);
                }
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

        public void GetInfoForContract(NpgsqlConnection con, ref Embauche e, ref Company c, ref Salary s, ref BddTitre[] avantages)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                avantages = new BddTitre("avantage").select(con).OfType<BddTitre>().ToArray<BddTitre>();
                e = e.Find(con);
                c = (Company)c.select(con)[0];
                s = (Salary)s.select("WHERE idembauche = " + e.Id, con)[0];
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
    }
}