using Npgsql;

namespace SIRH.Models
{   

    // pour l'affichage
    public class Salaire : BddObjet
    {
        int id;
        int idembauche;
        double valeurbrute;
        double valeurnet;
        DateTime datedebut;
        DateTime datefin;

        public override String tableName()
        {
            return "salaire";
        }
        public Salaire()
        {
            
        }
        public Salaire(int id,int idembauche,double valeurbrute,double valeurnet,DateTime datedebut,DateTime datefin)
        {
            this.Id = id;
            this.Idembauche = idembauche;
            this.Valeurbrute = valeurbrute;
            this.Valeurnet = valeurnet;
            this.Datedebut = datedebut;
            this.Datefin = datefin;
        }

        public int Id { get => id; set => id = value; }
        public int Idembauche { get => idembauche; set => idembauche = value; }
        public double Valeurbrute { 
            get => valeurbrute; 
            set{
                if(value < 0)
                {
                    valeurbrute = 0;
                }
                else
                {
                    valeurbrute = value;
                }
            }
        }
        public double Valeurnet { 
            get => valeurnet; 
            set
            {
                if(value < 0)
                {
                    valeurnet = 0;
                }
                else
                {
                    valeurnet = value;
                }
            }
        }
        public DateTime Datedebut {
            get => datedebut; 
            set {
                if (value == DateTime.MinValue)
                {
                    datedebut = DateTime.Now;
                }
                else
                {
                    datedebut = value;
                }
            }
        }
        public DateTime Datefin { get => datefin; set => datefin = value; }
    }

}