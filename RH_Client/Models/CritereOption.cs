using Npgsql;

namespace RH_Client.Models
{
    public class CritereOption : BddObjet
    {
        int id;
        int idcritere;
        string libelle;

        public int Id { get => id; set => id = value; }
        public int Idcritere { get => idcritere; set => idcritere = value; }
        public string Libelle { get => libelle; set => libelle = value; }

        public override String tableName()
        {
            return "critere_option";
        }

        public CritereOption(){
            this.init();
        }

        public CritereOption(int id,int idcritere,string libelle)
        {  
            this.id = id;
            this.idcritere = idcritere;
            this.libelle = libelle;
        }


    }

}