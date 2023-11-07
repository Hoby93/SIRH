using Npgsql;

namespace SIRH.Models
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

        public override String tableName()
        {
            return "besoin";
        }


        public void GetJob(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
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
                con = new Connect().getConnectPostgres();
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
    }
}