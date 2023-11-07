using Npgsql;

namespace SIRH.Models
{
    public class Conge : BddObjet
    {
        int id;
        int idEmbauche;
        int idTypeConge;
        DateTime debutConge;
        DateTime finConge;
        int etat;
        Embauche embauche;
        TypeConge typeConge;
        string table;

        public Conge()
        {
            this.init();
            this.table = "demandeConge";
        }

        public Conge(int id, int idEmbauche, int idTypeConge, DateTime debutConge, DateTime finConge, int etat)
        {
            Id = id;
            IdEmbauche = idEmbauche;
            IdTypeConge = idTypeConge;
            DebutConge = debutConge;
            FinConge = finConge;
            Etat = etat;
            this.table = "demandeConge";
        }

        public int Id { get => id; set => id = value; }
        public int IdEmbauche { get => idEmbauche; set => idEmbauche = value; }
        public int IdTypeConge { get => idTypeConge; set => idTypeConge = value; }
        public DateTime DebutConge { get => debutConge; set => debutConge = value; }
        public DateTime FinConge { get => finConge; set => finConge = value; }
        public int Etat { get => etat; set => etat = value; }
        public TypeConge TypeConge { get => typeConge; set => typeConge = value; }
        public Embauche Embauche { get => embauche; set => embauche = value; }

        public override string tableName()
        {
            return this.table;
        }

        public string ShowStatus()
        {
            if (DateTime.Now > this.FinConge)
            {
                return "<div class=\"badge badge-danger\">Termine</div>";
            }
            else if (this.DebutConge <= DateTime.Now && DateTime.Now <= this.FinConge)
            {
                return "<div class=\"badge badge-warning\">En cours</div>";
            }
            else
            {
                return "<div class=\"badge badge-success\">Cree</div>";
            }
        }

        public double Arrondir(double duree)
        {
            double partieEntiere = Math.Floor(duree);
            double decimale = duree - partieEntiere;
            if (decimale <= 0.25)
            {
                return partieEntiere;
            }
            else if (0.25 < decimale && decimale < 0.75)
            {
                return partieEntiere + 0.5;
            }
            return partieEntiere + 1;
        }

        public double GetDuree()
        {
            double duree = (this.FinConge - this.DebutConge).TotalDays;
            if (duree < 0)
            {
                duree = 0;
            }
            return this.Arrondir(duree);
        }

        public Conge[] GetDemandesConges(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                Conge[] demandes = this.select(con).OfType<Conge>().ToArray<Conge>();
                for (int i = 0; i < demandes.Length; i++)
                {
                    demandes[i].GetEmbauche(con);
                    demandes[i].GetTypeConge(con);
                }
                return demandes;
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

        public Conge[] GetReelConges(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                this.table = "reelConge";
                Conge[] reel = this.select(con).OfType<Conge>().ToArray<Conge>();
                for (int i = 0; i < reel.Length; i++)
                {
                    reel[i].GetEmbauche(con);
                    reel[i].GetTypeConge(con);
                }
                this.table = "demandeConge";
                return reel;
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

        public Conge[] GetConges(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                Conge[] demandes = this.GetDemandesConges(con);
                Conge[] reel = this.GetReelConges(con);
                Conge[] conges = new Conge[demandes.Length + reel.Length];
                int k = 0;
                for (int i = 0; i < demandes.Length; i++)
                {
                    conges[k] = demandes[i];
                    k++;
                }
                for (int i = 0; i < reel.Length; i++)
                {
                    conges[k] = reel[i];
                    k++;
                }
                return conges;
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

        public void GetEmbauche(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                Embauche e = new Embauche();
                e.Id = this.IdEmbauche;
                e = e.Find(con);
                this.Embauche = e;
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

        public void GetTypeConge(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                TypeConge tc = new TypeConge();
                tc = (TypeConge)tc.select("WHERE id = " + this.IdTypeConge, con)[0];
                this.TypeConge = tc;
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