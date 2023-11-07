using Npgsql;

namespace SIRH.Models
{
    public class Questions : BddObjet
    {
        int id;
        int idPoste;
        string question;
        double points;
        Proposal[] propositions;

        public Questions()
        {
            this.init();
        }

        public Questions(int id, int idPoste, string question, double point)
        {
            Id = id;
            IdPoste = idPoste;
            Question = question;
            Points = point;
        }

        public int Id { get => id; set => id = value; }
        public int IdPoste { get => idPoste; set => idPoste = value; }
        public string Question { get => question; set => question = value; }
        public double Points { get => points; set => points = value; }
        public Proposal[] Propositions { get => propositions; set => propositions = value; }

        public override string tableName()
        {
            return "question";
        }

        public void InsertPropositions(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                for (int i = 0; i < propositions.Length; i++)
                {
                    propositions[i].IdQuestion = this.Id;
                    propositions[i].insert(con);
                }
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