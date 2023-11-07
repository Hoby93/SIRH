using Npgsql;

namespace SIRH.Models
{
    public class Employe : Candidate
    {
        int idembauche;
        String service;
        String poste;
        int idbesoincandidat;
        int etat;
        DateTime dateembauche;

        public string Service { get => service; set => service = value; }
        public string Poste { get => poste; set => poste = value; }
        public DateTime Dateembauche { get => dateembauche; set => dateembauche = value; }
        public int Idbesoincandidat { get => idbesoincandidat; set => idbesoincandidat = value; }
        public int Etat { get => etat; set => etat = value; }
        public int Idembauche { get => idembauche; set => idembauche = value; }

        public Employe()
        {
            this.init();
        }

        public override String tableName()
        {
            return "vw_embauche";
        }

        public Employe(string service, string poste, int idbesoincandidat, DateTime dateembauche)
        {
            this.service = service;
            this.poste = poste;
            this.dateembauche = dateembauche;
            this.idbesoincandidat = idbesoincandidat;
        }

        public Object[] Criteres(NpgsqlConnection cnx)
        {
            Object[] criteres = new CandidatCritere().select($"where idcandidat = {this.idbesoincandidat}", cnx);

            return criteres;
        }

    }
}