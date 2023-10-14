using Npgsql;

namespace RH_Client.Models
{
    public class Connexion
    {
        public NpgsqlConnection getConnectPostgres()
        {
            try
            {

                string connString = "Server=localhost;Port=5432;Database=rh;User Id=postgres;Password=Dbamanager1;";
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