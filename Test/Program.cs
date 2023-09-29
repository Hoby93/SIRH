// See https://aka.ms/new-console-template for more information
using Test.Models;

class Program
{
    static void Main(string[] args)
    {

        /*
        SELECT():
            1. BddObjet.select(NpgsqlConnection connexion);
            2. BddObjet.select(String query_clause, NpgsqlConnection connexion);
        */
        Object[] employes = new Employe().select(null);

        foreach(Employe employe in employes) {
            Console.WriteLine(employe.Id);
            Console.WriteLine(employe.Nom);
            Console.WriteLine(employe.Prenom);
            Console.WriteLine(employe.Ip);
            Console.WriteLine(employe.Datenaissance);
            Console.WriteLine("------------------");
        }

        /*
        INSERT():
            - Les attributs qui n'ont pas de valeur sont definie 'default' pour l'insertion
            - Utiliser la fonction init() pour initialiser la valeur des attributs.
                ex: 
                Public Employe(String nom) {
                    this.init();
                    this.nom = nom;
                }
        */
        Employe eInsert1 = new Employe(120, "RAKOTO", "Nirina", 2000.56, new DateTime());
        Employe eInsert2 = new Employe("RASOA ", "Nirina");

        // eInsert1.insert(null);
        // eInsert2.insert(null);

        /*
        UPDATE():
            - La premiere attribut doit etre le cle primaire
            - Seul les attributs qui ont une valeur sont modifier dans la base
            - La condition de modification est simple, soit on le donne comme argument
            soit  la valeur du cle primaire de l'objet appelant comme reference

            1. BddObjet.update(NpgsqlConnection connexion);
            2. BddObjet.update(String condition, NpgsqlConnection connexion);
        */
        Employe eUpdate1 = new Employe(1, "RAKOTOBE", "Nirina", 2500.10, new DateTime(2023, 9, 30));
        Employe eUpdate2 = new Employe("NONE", "IP sup 3000");

        // eUpdate1.update(null);
        // eUpdate2.update("ip > 3000", null);

        /*
        HASRESULT()
        */
        Boolean hasResult = new BddObjet().hasResult("select * from employe where ip > 6000", null);
        Console.WriteLine(hasResult);

        /*
        GETDOUBLE()
        */
        double value =  new BddObjet().getDouble("select ip from employe where id = 2", null);
        Console.WriteLine(value);
    }
}