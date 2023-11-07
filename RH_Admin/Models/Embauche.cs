using Npgsql;

namespace SIRH.Models
{
    public class Embauche : Candidate
    {
        int idbesoincandidat;
        int idCandidat;
        int idPoste;
        int idTypeContrat;
        DateTime dateEmbauche;
        int etat;
        string numeromatricule;
        Poste poste;
        BddTitre contrat;

        public Embauche()
        {
            this.init();
        }

        public Embauche(int id, int idcandidat, int idposte, DateTime dateembauche, int idtypecontrat)
        {
            this.Id = id;
            this.IdCandidat = idcandidat;
            this.IdPoste = idposte;
            this.DateEmbauche = dateembauche;
            this.IdTypeContrat = idtypecontrat;
        }

        public Embauche(int id)
        {
            this.init();
            setNumeromatricule(id);
        }

        public Embauche(int idCandidat, int idPoste, int idTypeContrat, DateTime dateEmbauche)
        {
            IdCandidat = idCandidat;
            IdPoste = idPoste;
            IdTypeContrat = idTypeContrat;
            DateEmbauche = dateEmbauche;
        }

        public Embauche(int idbesoincandidat, int idCandidat, int idPoste, int idTypeContrat, DateTime dateEmbauche, int etat)
        {
            this.Id = -1;
            this.idbesoincandidat = idbesoincandidat;
            this.idCandidat = idCandidat;
            this.idPoste = idPoste;
            this.idTypeContrat = idTypeContrat;
            this.dateEmbauche = dateEmbauche;
            this.etat = etat;
        }

        public int IdCandidat { get => idCandidat; set => idCandidat = value; }
        public int IdPoste { get => idPoste; set => idPoste = value; }
        public int IdTypeContrat { get => idTypeContrat; set => idTypeContrat = value; }
        public DateTime DateEmbauche { get => dateEmbauche; set => dateEmbauche = value; }
        public Poste Poste { get => poste; set => poste = value; }
        public BddTitre Contrat { get => contrat; set => contrat = value; }
        public string Numeromatricule { get => numeromatricule; set => numeromatricule = value; }
        public int Idbesoincandidat { get => idbesoincandidat; set => idbesoincandidat = value; }
        public int Etat { get => etat; set => etat = value; }

        public override string tableName()
        {
            return "embauche";
        }

        public void setNumeromatricule(int num) {
            string matriculeNum = num.ToString("D4");
            
            Numeromatricule = "M" + matriculeNum;
            Console.WriteLine(Numeromatricule);
        }

        public Embauche Find(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                Embauche e = (Embauche)this.select("WHERE id = " + this.Id, con)[0];
                BddTitre bt = (BddTitre)new BddTitre("typecontrat").select("WHERE id = " + e.IdTypeContrat, con)[0];
                e.GetCandidat(con);
                e.GetPoste(con);
                e.Contrat = bt;
                return e;
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

        public Embauche[] GetEmbauchesWithoutContract(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                List<Embauche> em = new List<Embauche>();
                string query = "SELECT embauche.*, contratEssai.id AS idContratEssai FROM embauche " +
                "LEFT JOIN contratEssai ON contratEssai.idbesoincandidat = embauche.id " +
                "WHERE contratEssai.id IS NULL";
                Console.WriteLine(query);
                using (var command = new NpgsqlCommand(query, con))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Embauche e = new Embauche(reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetDateTime(4));
                            e.Id = reader.GetInt32(0);
                            em.Add(e);
                        }
                    }
                }
                foreach (var item in em)
                {
                    item.GetCandidat(con);
                    item.GetPoste(con);
                }
                return em.ToArray();
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

        public Embauche[] GetAll(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                Embauche[] embauches = this.select(con).OfType<Embauche>().ToList().ToArray<Embauche>();
                foreach (var item in embauches)
                {
                    item.GetCandidat(con);
                    item.GetPoste(con);
                }
                return embauches;
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

        public void GetPoste(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                Poste p = new Poste();

                p.Id = this.IdPoste;
                p = (Poste)p.select("WHERE id = " + p.Id, con)[0];
                p.GetService(con);
                this.Poste = p;
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

        public void GetCandidat(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                Candidate c = new Candidate();
                c.Id = this.IdCandidat;
                c = (Candidate)c.select("WHERE id = " + c.Id, con)[0];
                this.Nom = c.Nom;
                this.Prenom = c.Prenom;
                this.Telephone = c.Telephone;
                this.Email = c.Email;
                this.Adresse = c.Adresse;
                this.Genre = c.Genre;
                this.DateNaissance = c.DateNaissance;
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