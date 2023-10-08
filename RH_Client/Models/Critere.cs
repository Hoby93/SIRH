using Npgsql;

namespace clientGRH.Models
{
    public class Critere : BddObjet
    {
        int id;
        string libelle;
        int nature;

        public Critere() {
            this.init();
        }


        public int Id { get => id; set => id = value; }
        public string Libelle { get => libelle; set => libelle = value; }
        public int Nature { get => nature; set => nature = value; }

        public override String tableName()
        {
            return "critere";
        }

        public Object[] Options() {
            string query_clauses = $"where idcritere = {this.Id}";
            Object[] options = new BddTitre("critere_option").select(query_clauses, null);

            return options;
        }

        public Critere(int id,string libelle,int nature) {
            this.id = id;
            this.libelle = libelle;
            this.nature = nature;
        }

        public List<Critere> getCritereByBesoin(NpgsqlConnection c,int idbesoin){
            bool flag = false;
            if(c == null){
                c = new Connexion().getConnectPostgres();
            }
            List<Critere> valiny = new List<Critere>();
            string queryString = "SELECT critere.* FROM critere JOIN besoin_critere ON critere.id = besoin_critere.idcritere WHERE besoin_critere.idbesoin ="+idbesoin;
            using (NpgsqlCommand command = new NpgsqlCommand (queryString, c))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["id"]) ;
                        string libelle = reader["libelle"].ToString();
                        int nature = Convert.ToInt32(reader["nature"]) ;
                        valiny.Add(new Critere(id,libelle,nature));
                    }
                }
            }

            if(flag){
                c.Close();
            }

            return valiny;
        }

        public CritereOption[] getCritereOption(NpgsqlConnection c){
            bool flag = false;
            if(c == null){
                c = new Connexion().getConnectPostgres();
            }

            CritereOption co = new CritereOption();
            Object[] val = co.select("WHERE idcritere = "+this.Id,c);

            CritereOption[] farany = val.Cast<CritereOption>().ToArray();

            if(flag){
                c.Close();
            }

            return farany;
        }
    }
}