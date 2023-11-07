using Microsoft.AspNetCore.Routing.Matching;
using Npgsql;
using RH_Client.Models;

namespace RH_Client.Models
{
    public class Embauche : BddObjet
    {
        int id;
        int idcandidat;
        int idposte;
        DateTime dateembauche;
        int idtypecontrat;

        public Embauche() {
            this.init();
        }

        public Embauche(int id,int idcandidat,int idposte,DateTime dateembauche,int idtypecontrat)
        {
            this.Id = id;
            this.Idcandidat = idcandidat;
            this.Idposte = idposte;
            this.Dateembauche = dateembauche;
            this.Idtypecontrat = idtypecontrat;
        }

        public int Id { get => id; set => id = value; }
        public int Idcandidat { get => idcandidat; set => idcandidat = value; }
        public int Idposte { get => idposte; set => idposte = value; }
        
        public DateTime Dateembauche { get => dateembauche; set => dateembauche = value; }
        public int Idtypecontrat { get => idtypecontrat; set => idtypecontrat = value; }

        public override String tableName()
        {
            return "embauche";
        }

        public static bool estEmploye(NpgsqlConnection con,int idcandidat){
            bool flag = false;
            if(con == null){
                con = new Connexion().getConnectPostgres();
                flag = true;
            }

            bool valiny = false;

            Object[] val = new Embauche().select($"WHERE idcandidat = {idcandidat}",con);
            if(val.Length > 0){
                valiny = true;
            }

            if(flag){
                con.Close();
            }
            return valiny;
        }

    }

}