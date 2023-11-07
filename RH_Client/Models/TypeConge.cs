namespace RH_Client.Models
{
    public class TypeConge : BddObjet
    {
        int id;
        String nom;
        int estDeductible;

        public TypeConge() {
            this.init();
        }

         public override String tableName()
        {
            return "typeConge";
        }

        public TypeConge(int id,String nom,int estDeductible)
        {
            this.Id = id;
            this.Nom = nom;
            this.EstDeductible = estDeductible;
        }


        public int Id { get => id; set => id = value; }
        public String Nom { get => nom; set => nom = value; }
        public int EstDeductible { get => estDeductible; set => estDeductible = value; }
    }
}