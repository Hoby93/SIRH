namespace SIRH.Models
{
    public class ContratEssaiAvantage : BddObjet
    {
        int id;
        int idContratEssai;
        int idAvantage;

        public ContratEssaiAvantage()
        {
            this.init();
        }

        public ContratEssaiAvantage(int id, int idContratEssai, int idAvantage)
        {
            Id = id;
            IdContratEssai = idContratEssai;
            IdAvantage = idAvantage;
        }

        public int Id { get => id; set => id = value; }
        public int IdContratEssai { get => idContratEssai; set => idContratEssai = value; }
        public int IdAvantage { get => idAvantage; set => idAvantage = value; }

        public override string tableName()
        {
            return "contratEssaiAvantage";
        }
    }
}