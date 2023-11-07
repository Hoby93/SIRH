using Npgsql;

namespace SIRH.Models
{
    public class Critere : BddObjet
    {
        int id;
        string libelle;
        int nature;

        public Critere()
        {
            this.init();
        }

        public Critere(int id, string libelle, int nature)
        {
            Id = id;
            Libelle = libelle;
            Nature = nature;
        }

        public int Id { get => id; set => id = value; }
        public string Libelle { get => libelle; set => libelle = value; }
        public int Nature { get => nature; set => nature = value; }

        public override String tableName()
        {
            return "critere";
        }

        public Option GetMinOptionDefault(Besoin besoin, NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                string query = "SELECT * FROM vw_besoin_critere_option_note " +
                "WHERE idcritere = " + this.Id + " AND idbesoincritere IS NULL AND note = (SELECT MIN(note) " +
                "FROM vw_besoin_critere_option_note " +
                "WHERE idcritere = " + this.Id + " AND idbesoincritere IS NULL) ";
                using (var command = new NpgsqlCommand(query, con))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Option(reader.GetInt32(2), reader.GetInt32(4), reader.GetString(5), reader.GetDouble(3));
                        }
                    }
                }
                return null;
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

        public Option GetMinOptionCustom(Besoin besoin, NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                string query = "SELECT vw_besoin_critere_option_note.*, besoin_critere.idbesoin, besoin_critere.coeff " +
                "FROM vw_besoin_critere_option_note " +
                "JOIN besoin_critere ON besoin_critere.id = idbesoincritere " +
                "WHERE vw_besoin_critere_option_note.idcritere = " + this.Id + " AND besoin_critere.idbesoin = " + besoin.Id + " AND note = (SELECT MIN(note) " +
                "FROM vw_besoin_critere_option_note " +
                "JOIN besoin_critere ON besoin_critere.id = idbesoincritere " +
                "WHERE vw_besoin_critere_option_note.idcritere = " + this.Id + " AND besoin_critere.idbesoin = " + besoin.Id + " AND note >= 0)";
                using (var command = new NpgsqlCommand(query, con))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Option(reader.GetInt32(2), reader.GetInt32(4), reader.GetString(5), reader.GetDouble(3));
                        }
                    }
                }
                return null;
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

        public Option GetMinOption(Besoin besoin, NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                Option defaut = this.GetMinOptionDefault(besoin, con);
                Option perso = this.GetMinOptionCustom(besoin, con);
                if (perso != null)
                {
                    return perso;
                }
                return defaut;
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

        public Object[] Options()
        {
            string query_clauses = $"where idcritere = {this.Id}";
            Object[] options = new BddTitre("critere_option").select(query_clauses, null);

            return options;
        }
    }
}