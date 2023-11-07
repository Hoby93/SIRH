using Npgsql;

namespace SIRH.Models
{
    public class Announcement : BddObjet
    {
        int id;
        int idBesoin;
        DateTime dateEnvoi;
        int idEntreprise;
        Besoin besoin;
        Company entreprise;
        Profile[] profilMinimal;

        public Announcement()
        {
            this.init();
        }

        public Announcement(int id, int idBesoin, DateTime dateEnvoi, int idEntreprise, Besoin besoin, Company entreprise, Profile[] profilMinimal)
        {
            Id = id;
            IdBesoin = idBesoin;
            DateEnvoi = dateEnvoi;
            IdEntreprise = idEntreprise;
            Besoin = besoin;
            Entreprise = entreprise;
            ProfilMinimal = profilMinimal;
        }

        public override String tableName()
        {
            return "annonce";
        }

        public int Id { get => id; set => id = value; }
        public int IdBesoin { get => idBesoin; set => idBesoin = value; }
        public DateTime DateEnvoi { get => dateEnvoi; set => dateEnvoi = value; }
        public int IdEntreprise { get => idEntreprise; set => idEntreprise = value; }
        public Besoin Besoin { get => besoin; set => besoin = value; }
        public Company Entreprise { get => entreprise; set => entreprise = value; }
        public Profile[] ProfilMinimal { get => profilMinimal; set => profilMinimal = value; }

        public Announcement[] GetAll(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                Announcement[] annonces = this.select(con).OfType<Announcement>().ToList().ToArray<Announcement>();
                Company c = (Company)new Company().select(con)[0];
                foreach (var item in annonces)
                {
                    item.GetNeed(con);
                    item.Entreprise = c;
                    item.GetAllProfile(con);
                }
                return annonces;
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

        public Critere[] GetAllCritere(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                List<Critere> criteres = new List<Critere>();
                string query = "SELECT besoin_critere.*, critere.libelle, critere.nature FROM besoin_critere JOIN critere ON critere.id = besoin_critere.idcritere";
                Console.WriteLine(query);
                using (var command = new NpgsqlCommand(query, con))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            criteres.Add(new Critere(reader.GetInt32(2), reader.GetString(4), reader.GetInt32(5)));
                        }
                    }
                }
                return criteres.ToArray();
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

        public void GetAllProfile(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                Critere[] criteres = this.GetAllCritere(con);
                this.ProfilMinimal = new Profile[criteres.Length];
                for (int i = 0; i < criteres.Length; i++)
                {
                    this.ProfilMinimal[i] = new Profile();
                    this.ProfilMinimal[i].Critere = criteres[i];
                    this.ProfilMinimal[i].Option = this.ProfilMinimal[i].Critere.GetMinOption(this.Besoin, con);
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

        public void GetNeed(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                this.Besoin = (Besoin)new Besoin().select("WHERE id = " + this.IdBesoin, con)[0];
                this.Besoin.GetJob(con);
                this.Besoin.GetContract(con);
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

        public void Generate(NpgsqlConnection con, Besoin besoin, DateTime date)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                this.IdBesoin = besoin.Id;
                if (date == DateTime.MinValue)
                {
                    this.DateEnvoi = DateTime.Now;
                }
                else
                {
                    this.DateEnvoi = date;
                }
                this.IdEntreprise = 1;
                this.insert(con);
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