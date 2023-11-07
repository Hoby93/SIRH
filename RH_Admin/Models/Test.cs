using Npgsql;

namespace SIRH.Models
{
    public class Test
    {
        Questions[] questions;

        public Test()
        {
        }

        public Test(Questions[] questions)
        {
            Questions = questions;
        }

        public Questions[] Questions { get => questions; set => questions = value; }

        public void Create(NpgsqlConnection con)
        {
            bool isNewConnexion = false;
            if (con == null)
            {
                con = new Connect().getConnectPostgres();
                isNewConnexion = true;
            }
            try
            {
                Besoin b = new Besoin();
                b = (Besoin)b.select("WHERE id = (SELECT MAX(id) FROM besoin)", con)[0];
                for (int i = 0; i < Questions.Length; i++)
                {
                    questions[i].IdPoste = b.Idposte;
                    Questions[i].insert(con);
                    Questions[i].Id = Questions[i].getInteger("select max(id) from question", con);
                    Questions[i].InsertPropositions(con);
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