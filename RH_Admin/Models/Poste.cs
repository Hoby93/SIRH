using Npgsql;

namespace SIRH.Models
{
    public class Poste : BddObjet
    {
        int id;
        int idservice;
        string libelle;
        string description;
        BddTitre service;


        public Poste()
        {
            this.init();
        }

        public Poste(int id, int idservice, string libelle, string description)
        {
            this.id = id;
            this.idservice = idservice;
            this.libelle = libelle;
            this.description = description;
        }

        public int Id { get => id; set => id = value; }
        public int Idservice { get => idservice; set => idservice = value; }
        public string Libelle { get => libelle; set => libelle = value; }
        public string Description { get => description; set => description = value; }
        public BddTitre Service { get => service; set => service = value; }

        public override String tableName()
        {
            return "poste";
        }

        public void GetService(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                this.Service = new BddTitre("services").select("WHERE id = " + this.Idservice, con)[0] as BddTitre;
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