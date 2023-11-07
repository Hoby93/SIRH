using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MySqlConnector;
using Npgsql;

namespace SIRH.Models
{
    internal class Connect
    {
        public NpgsqlConnection getConnectPostgres()
        {
            try
            {

                string connString = "Server=localhost;Port=5432;Database=sirh;User Id=postgres;Password=ITUprom15";
                Console.Write("Connecting to PostgreSQL Server ... ");
                NpgsqlConnection connection = new NpgsqlConnection(connString);
                Console.WriteLine(connection);
                connection.Open();
                Console.WriteLine("Done.");
                
                return connection;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public MySqlConnection GetMySqlConnection()
        {
            try
            {
                string connString = "Server=localhost;user=root;password=root;database=katsaka;";
                Console.Write("Connecting to MySQL Server ... ");
                MySqlConnection connection = new MySqlConnection(connString);
                Console.WriteLine(connection);
                connection.Open();
                Console.WriteLine("Done.");
                return connection;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public SqlConnection getConnectMSSQL()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "localhost";
                builder.InitialCatalog = "master";
                builder.IntegratedSecurity = true;
                builder.TrustServerCertificate = true;
                Console.Write("Connecting to SQL Server ... ");
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                connection.Open();
                string sql = "USE katsaka;";
                SqlCommand com = new SqlCommand(sql, connection);
                com.ExecuteNonQuery();
                Console.WriteLine("Done.");
                return connection;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
