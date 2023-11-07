namespace SIRH.Models
{
    public class TypeConge : BddObjet
    {
        int id;
        string nom;
        int estDeductible;

        public TypeConge()
        {
            this.init();
        }

        public TypeConge(int id, string nom, int estDeductible)
        {
            Id = id;
            Nom = nom;
            EstDeductible = estDeductible;
        }

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public int EstDeductible { get => estDeductible; set => estDeductible = value; }

        public override string tableName()
        {
            return "typeconge";
        }
    }
}