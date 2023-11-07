using Npgsql;

namespace SIRH.Models
{
    public class Services : BddObjet
    {
        public int id{get;set;}
        public string libelle;

        public string Libelle
        {
            get { return libelle; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Le libellé ne peut pas être vide.");
                }
                libelle = value;
            }
        }

        public override String tableName(){
            return "services";
        }

        public Services()
        {   
            
        }

        public Services(string libelle)
        {   
            this.init();
            this.Libelle = libelle;
        }

        public Services(int id)
        {
            this.id = id;
        }

        public Services(int id,string libelle)
        {   
            this.id = id;
            this.Libelle = libelle;
        }

        public Services[] getAllServices(NpgsqlConnection c){
            bool flag = false;
            if(c == null){
                c = new Connexion().getConnectPostgres();
            }

            Object[] val = this.select(c);

            Services[] farany = val.Cast<Services>().ToArray();

            if(flag){
                c.Close();
            }

            return farany;
        }

        public Services getServiceById(NpgsqlConnection c,int id)
        {
            bool flag = false;
            if(c == null){
                c = new Connexion().getConnectPostgres();
            }

            Object[] val = this.select("WHERE id = "+id,c);

            Services[] farany = val.Cast<Services>().ToArray();

            if(flag){
                c.Close();
            }

            return farany[0];
        }

        public void supprimerService(NpgsqlConnection c)
        {
            bool flag = false;
            if(c == null){
                c = new Connexion().getConnectPostgres();
            }

            try
            {
                string deleteQuery = "DELETE FROM services WHERE id = @ServiceId";
                using (NpgsqlCommand cmd = new NpgsqlCommand(deleteQuery, c))
                {
                    cmd.Parameters.AddWithValue("@ServiceId", this.id);
                    cmd.ExecuteNonQuery(); // Exécutez la commande DELETE.
                }
            }
            finally
            {
                if(flag)
                {
                c.Close();
                }
            }

            
        }
    }
}