using Npgsql;

namespace RH_Client.Models
{
    public class Candidate : BddObjet
    {
        int id;
        string nom;
        string prenom;
        int genre;
        DateTime dateNaissance;
        string telephone;
        string email;
        string adresse;

        public Candidate()
        {
            this.init();
        }

        public Candidate(int idcandidat)
        {
            this.init();
            this.id = idcandidat;
        }


        public Candidate(int id, string nom, string prenom, int genre, DateTime dtn, string telephone, string email, string adresse)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Genre = genre;
            DateNaissance = dtn;
            Telephone = telephone;
            Email = email;
            Adresse = adresse;
        }

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Email { get => email; set => email = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public DateTime DateNaissance { get => dateNaissance; set => dateNaissance = value; }
        public int Genre { get => genre; set => genre = value; }

        public override string tableName()
        {
            return "candidat";
        }

        public void Postuler(NpgsqlConnection con, int idBesoin, int[] idCritereOption)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connexion().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                BesoinCandidat bc = new BesoinCandidat();
                bc.IdBesoin = idBesoin;
                bc.IdCandidat = this.Id;
                bc.DateInscription = DateTime.Now;
                bc.insert(con);
                this.InsertProfile(con, idCritereOption);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (isNewConnexion == true)
                {
                    con.Close();
                }
            }
        }

        public void InsertProfile(NpgsqlConnection con, int[] critereOption)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connexion().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                CandidatCritere cc = new CandidatCritere();
                cc.IdCandidat = this.getInteger("select max(id) from besoin_candidat", null);// satria idcandidat ao am candidat critere tsy idcandidat fa idbesoincandidat
                foreach (var item in critereOption)
                {
                    cc.IdCritereOption = item;
                    cc.insert(con);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
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