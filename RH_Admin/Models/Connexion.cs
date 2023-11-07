using Npgsql;

namespace SIRH.Models
{
    public class Connexion
    {
        public NpgsqlConnection getConnectPostgres()
        {
            try
            {

                string connString = "Server=localhost;Port=5432;Database=sirh;User Id=postgres;Password=ITUprom15;";
                // Console.Write("Connecting to PostgreSQL Server ... ");
                NpgsqlConnection connection = new NpgsqlConnection(connString);
                // Console.WriteLine(connection);
                connection.Open();
                // Console.WriteLine("Done.");
                return connection;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}