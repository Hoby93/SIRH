using Npgsql;

namespace RH_Client.Models
{
    public class DemandeConge : BddObjet
    {
        int id;
        int idembauche;
        int idtypeconge;
        DateTime debutconge;
        DateTime finconge;
        int etat;

        public DemandeConge() {
            this.init();
        }

         public override String tableName()
        {
            return "demandeConge";
        }

        public DemandeConge(int id,int idembauche,int idtypeconge,DateTime debutconge,DateTime finconge,int etat)
        {
            this.Id = id;
            this.Idembauche = idembauche;
            this.Idtypeconge = idtypeconge;
            this.Debutconge = debutconge;
            this.Finconge = finconge;
            this.Etat = etat;
            if(this.Debutconge > this.Finconge){
                throw new Exception("tsy mety debut > fin");
            }
        }


        public int Id { get => id; set => id = value; }
        public int Idembauche { get => idembauche; set => idembauche = value; }
        public int Idtypeconge { get => idtypeconge; set => idtypeconge = value; }
        public DateTime Debutconge { get => debutconge; set => debutconge = value; }
        public DateTime Finconge { get => finconge; set => finconge = value; }
        public int Etat { get => etat; set => etat = value; }

        public Embauche getEmbauche(NpgsqlConnection con){
            Embauche e = null;
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connexion().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                e = (Embauche) new Embauche().select($"WHERE id = {this.Idembauche}",con)[0];
            }
            finally
            {
                if (isNewConnexion == true)
                {
                    con.Close();
                }
            }
            return e;
        }

        //365j mbola statique
        public bool employeUnAnEtPlus(NpgsqlConnection con){
            bool valiny = false;

            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connexion().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                Embauche embauche = this.getEmbauche(con);
                TimeSpan diff = DateTime.Now - embauche.Dateembauche;
                if(diff.TotalDays >= 365){
                    valiny = true;
                }
            }
            finally
            {
                if (isNewConnexion == true)
                {
                    con.Close();
                }
            }

            return valiny;
        }


        //2.5 j mbola statique
        public double getTotalCongeJours(NpgsqlConnection con){
            double reste = 0;
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connexion().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                Embauche embauche = this.getEmbauche(con);
                TimeSpan diff = DateTime.Now - embauche.Dateembauche;

                // Console.WriteLine(diff.TotalDays);
                reste = ((int)diff.TotalDays/30)*2.5;
            }
            finally
            {
                if (isNewConnexion == true)
                {
                    con.Close();
                }
            }
            return reste;
        }

        // conge efa nataonle olona,izay efa vita(en jours)
        public double totalCongeDejaPrisJours(NpgsqlConnection con){
            double total = 0;
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connexion().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                ReelConge[] rc = new ReelConge().select($"WHERE idembauche = {this.Idembauche} AND finconge IS NOT NULL",con).OfType<ReelConge>().ToArray();
                for (int i = 0; i < rc.Length; i++)
                {
                    TypeConge refl = (TypeConge)new TypeConge().select($"WHERE id = {rc[i].Idtypeconge}",con)[0];
                    if(refl.EstDeductible == 1){
                        TimeSpan diff = rc[i].Finconge - rc[i].Debutconge;
                        total += diff.TotalDays+1;
                    }
                }
            }
            finally
            {
                if (isNewConnexion == true)
                {
                    con.Close();
                }
            }

            return total;
        }

        // reste conge anle olona amzay 
        // atao - le nbjour hananana sy le conge efa natao
        // raha sup 90 de 90 ihany ny valiny
        public double resteConge(NpgsqlConnection con){
            double reste = 0;
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connexion().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                reste = this.getTotalCongeJours(con) - totalCongeDejaPrisJours(con);
                if(reste > 90){
                    reste = 90;
                }
            }
            finally
            {
                if (isNewConnexion == true)
                {
                    con.Close();
                }
            }

            return reste;
        }

        public bool estDeductible(NpgsqlConnection con){
            bool valiny = false;
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connexion().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                TypeConge refl = (TypeConge)new TypeConge().select($"WHERE id = {this.Idtypeconge}",con)[0];
                if(refl.EstDeductible == 1){
                    valiny = true;
                }
            }
            finally
            {
                if (isNewConnexion == true)
                {
                    con.Close();
                }
            }

            return valiny;
        }

        public void insererDemande(NpgsqlConnection con){
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connexion().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                if(this.estDeductible(con) == false){
                    this.insert(con);
                } else {

                    if(this.employeUnAnEtPlus(con) == false){
                        throw new Exception("tsy mbola mahazo maka conge raha < 365jrs");
                    }
                    double total = this.getTotalCongeJours(con);
                    double pris = this.totalCongeDejaPrisJours(con);

                    double reste = this.resteConge(con);
                    Console.WriteLine($"total : {total} | pris : {pris}");
                    if((this.Finconge - this.Debutconge).TotalDays > reste){
                        throw new Exception($"tsy ampy ny conge, il ne vous reste que {reste} jours");
                    }

                    this.insert(con);
                    
                }
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