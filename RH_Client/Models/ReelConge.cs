using Npgsql;

namespace RH_Client.Models
{
    public class ReelConge : BddObjet
    {
        int id;
        int idembauche;
        int idtypeconge;
        DateTime debutconge;
        DateTime finconge;

        public ReelConge() {
            this.init();
        }

        public override String tableName()
        {
            return "reelConge";
        }

        public ReelConge(int id,int idembauche,int idtypeconge,DateTime debutconge,DateTime finconge)
        {
            this.Id = id;
            this.Idembauche = idembauche;
            this.Idtypeconge = idtypeconge;
            this.Debutconge = debutconge;
            this.Finconge = finconge;
            if(this.Debutconge > this.Finconge){
                throw new Exception("tsy mety debut > fin");
            }
        }

        public int Id { get => id; set => id = value; }
        public int Idembauche { get => idembauche; set => idembauche = value; }
        public int Idtypeconge { get => idtypeconge; set => idtypeconge = value; }
        public DateTime Debutconge { get => debutconge; set => debutconge = value; }
        public DateTime Finconge { get => finconge; set => finconge = value; }


    }
}