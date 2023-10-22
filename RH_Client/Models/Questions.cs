using Npgsql;

namespace RH_Client.Models
{
    public class Questions : BddObjet
    {
        int id;
        int idposte;
        string question;
        int point;

        public Questions() {
            this.init();
        }

        public Object[] GetPropositions(NpgsqlConnection cnx) {
            Object[] propositions = new Proposition().select($"where idquestion = {this.id}", cnx);

            return propositions;
        }

        public Questions(int id, int idposte, string question, int point)
        {
            this.id = id;
            this.idposte = idposte;
            this.question = question;
            this.point = point;
        }

        public int Id { get => id; set => id = value; }
        public int Idposte { get => idposte; set => idposte = value; }
        public string Question { get => question; set => question = value; }
        public int Points { get => point; set => point = value; }

        public override String tableName()
        {
            return "question";
        }

        public static Questions[] getAllQuestions(int idposte)
        {
            Object[] questions = new Questions().select($"where idposte = {idposte}", null);

            return questions.OfType<Questions>().ToArray();
        }

        public Proposition[] getProposition()
        {
            Object[] proposition = new Proposition().select($"where idquestion = {this.Id}", null);

            return proposition.OfType<Proposition>().ToArray();
        }

        public int nbBonneRep()
        {
            Object[] proposition = new Proposition().select($"where idquestion = {this.Id} and etat = 1", null);

            return proposition.Length;
        }

        public VwCandidatReponse[] getCandidatReponses(int idbesoincandidat)
        {
            Object[] rep = new VwCandidatReponse().select($"where idquestion = {this.Id} and idbesoincandidat = {idbesoincandidat}", null);

            return rep.OfType<VwCandidatReponse>().ToArray();
        }

        public double getScore(int idbesoincandidat)
        {
            double note = 0;

            Proposition[] propositions = this.getProposition();

            int ct = 0;
            for (int i = 0; i < propositions.Length; i++)
            {
                
                VwCandidatReponse[] cr = this.getCandidatReponses(idbesoincandidat);
                if(cr.Length != this.nbBonneRep())
                {
                    break;
                }
                for (int j = 0; j < cr.Length; j++)
                {
                    if(propositions[i].Etat == 1 && propositions[i].Id == cr[j].Idproposition){
                        ct += 1;
                    }
                }
            }
            if(ct == this.nbBonneRep()){
                note += this.Points;
            }
            
            return note;
        }

        public double noteTest(int idposte,int idbesoincandidat)
        {
            Questions[] allQuestions = Questions.getAllQuestions(idposte); 

            double total = 0;
            
            for (int i = 0; i < allQuestions.Length; i++)
            {
                total += allQuestions[i].getScore(idbesoincandidat);
            }
            
            return total;
        }

    }
}