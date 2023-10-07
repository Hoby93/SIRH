using Npgsql;
using System.Reflection;

namespace SIRH.Models
{
    public class BddObjet
    {
        String db = "sirh";
        String user = "postgres";
        String pwd = "ITUprom15";

        // Avoir le nom de table
        public virtual String tableName()
        {
            return "";
        }

        // Initialiser les types primitifs 
        public void init()
        {
            Type type = this.GetType();
            PropertyInfo[] propreties = type.GetProperties();

            foreach(PropertyInfo proprety in propreties)
            {
                if(proprety.PropertyType == typeof(int) || proprety.PropertyType == typeof(double)) {
                    proprety.SetValue(this, -1);
                }
            }
        }

        /** 
            Script_Utilitaire
        **/

        // Avoir tout les colonnes d'une table
        public List<string> getColumnOfDatabase(NpgsqlConnection connection)
        {
            List<string> columnNames = new List<string>();

            string query = $"SELECT * FROM {tableName()} LIMIT 1";
            using (var command = new NpgsqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string columnName = reader.GetName(i);
                        columnNames.Add(columnName);
                    }
                }
            }

            return columnNames;
        }

        // Avoir les colonnes a la fois attributs
        public List<string> getFieldsOnDatabase(NpgsqlConnection connection)
        {
            Type type = this.GetType();
            List<string> ans = new List<string>();
            PropertyInfo[] propreties = type.GetProperties();
            List<String> columnNames = getColumnOfDatabase(connection);

            foreach(PropertyInfo proprety in propreties)
            {
                foreach (string columnName in columnNames)
                {
                    if(string.Equals(proprety.Name, columnName, StringComparison.OrdinalIgnoreCase)) { 
                        ans.Add(proprety.Name);
                    }
                }
            }

            return ans;
        }

        // Avoir la valeur d'un attribut 
        public Object getProprety(String argName) 
        {
            Type type = this.GetType();
            PropertyInfo proprety = type.GetProperty(argName);

            return proprety.GetValue(this);
        }

        // Modifier la valeur d'un attribut
        public void setProprety(Object objectInstance, Object arg, String argName) 
        {
            Type type = this.GetType();
            PropertyInfo proprety = type.GetProperty(argName);
            proprety.SetValue(objectInstance, arg);
        }

        // Generer le format appropriee a une valeur
        public String forQueryValue(Object arg)
        {
            String ans = $"'{arg}'".Replace(',', '.');

            if(arg is int || arg is double)
            {
                ans = $"{arg}".Replace(',', '.');
            }

            return ans;
        }

        // Generer les colonnes pour son ordres d'insertion
        public String scriptInsertColumns(List<string> columnNames)
        {
            String ans = "";

            for(int i = 0; i < columnNames.Count; i++)
            {
                ans += columnNames[i];
                if(i != columnNames.Count - 1)
                {
                    ans += ",";
                }
            }

            return ans;
        }

        // Genere les valeurs pour l'insertion
        public String scriptInsertValues(NpgsqlConnection connection)
        {
            List<String> columnNames = getFieldsOnDatabase(connection);
            String ans = "";

            for (int i = 0; i < columnNames.Count; i++)
            {
                Object arg = getProprety(columnNames[i]);
                if (arg == null || arg.ToString().Equals("-1") || arg.ToString().Equals("01/01/0001 00:00:00"))
                {
                    ans += "default";
                }
                else
                {
                    ans += forQueryValue(arg);
                }
                if (i != columnNames.Count - 1)
                {
                    ans += ",";
                }
            }



            return ans;
        }

        // Generer la condition approprie a une modification
        public String scriptModifCondition(String condition, String key) {
            if(condition != "") {
                return condition;
            }
            return $"{key} = {getProprety(key)}";
        }



        /** 
            Script_Principal 
        **/



        public Object[] select(String query_clauses, NpgsqlConnection connection)
        {
            bool shouldCloseConnection = false;

            // Create a new connection if the provided connection is null
            if (connection == null)
            {
                string connectionString = $"Server=localhost;Port=5432;Database={db};User Id={user};Password={pwd};";
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
                shouldCloseConnection = true;
            }

            List<Object> ans = new List<Object>();
            List<String> attributsName = getFieldsOnDatabase(connection);

            string query = "SELECT * FROM " + tableName() + " " + query_clauses;
            using (var command = new NpgsqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Object objectInstance = Activator.CreateInstance(this.GetType());
                        foreach (String attributName in attributsName)
                        {
                            int colNum = reader.GetOrdinal(attributName);
                            if(!reader.IsDBNull(colNum)) {
                                this.setProprety(objectInstance, reader.GetValue(colNum), attributName);
                            }
                        }
                        ans.Add(objectInstance);
                    }
                }
            }

            // Close the connection if it was created within this method
            if (shouldCloseConnection)
            {
                connection.Close();
            }

            return ans.ToArray();
        }

        public Object[] select(NpgsqlConnection connection)
        {
            return this.select("", connection);
        }

        public void update(String condition, NpgsqlConnection connection)
        {
            bool shouldCloseConnection = false;

            // Create a new connection if the provided connection is null
            if (connection == null)
            {
                string connectionString = $"Server=localhost;Port=5432;Database={db};User Id={user};Password={pwd};";
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
                shouldCloseConnection = true;
            }

            List<String> columnNames = getFieldsOnDatabase(connection);
            String key = columnNames[0];

            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                command.Connection = connection;
                int rowsAffected = 0;

                foreach (string col in columnNames)
                {
                    // Invoke the method and return the result
                    Object value = getProprety(col);
                    if (value != null && !value.ToString().Equals("-1") && !value.ToString().Equals("01/01/0001 00:00:00")) {
                        string updateQuery = $"UPDATE {tableName()} SET {col} = {forQueryValue(value)} WHERE {scriptModifCondition(condition,key)}";
                        //Console.WriteLine(updateQuery);
                        command.CommandText = updateQuery;

                        rowsAffected += command.ExecuteNonQuery();
                    }
                }
            }

            // Close the connection if it was created within this method
            if (shouldCloseConnection)
            {
                connection.Close();
            }
        }

        public void update(NpgsqlConnection connection)
        {
            this.update("", connection);
        }

        public void delete(String condition, NpgsqlConnection connection)
        {
            bool shouldCloseConnection = false;

            // Create a new connection if the provided connection is null
            if (connection == null)
            {
                string connectionString = $"Server=localhost;Port=5432;Database={db};User Id={user};Password={pwd};";
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
                shouldCloseConnection = true;
            }

            List<String> columnNames = getFieldsOnDatabase(connection);
            String key = columnNames[0];

            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                command.Connection = connection;
                int rowsAffected = 0;

                // Invoke the method and return the result
                string updateQuery = $"delete from {tableName()} where {scriptModifCondition(condition, key)}";
                Console.WriteLine(updateQuery);
                // command.CommandText = updateQuery;

                // rowsAffected += command.ExecuteNonQuery();
            }

            // Close the connection if it was created within this method
            if (shouldCloseConnection)
            {
                connection.Close();
            }
        }

        public void delete(NpgsqlConnection connection)
        {
            this.delete("", connection);
        }

        public void insert(NpgsqlConnection connection)
        {
            bool shouldCloseConnection = false;

            // Create a new connection if the provided connection is null
            if (connection == null)
            {
                string connectionString = $"Server=localhost;Port=5432;Database={db};User Id={user};Password={pwd};";
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
                shouldCloseConnection = true;
            }

            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                command.Connection = connection;
                int rowsAffected = 0;

                // Invoke the method and return the result
                String columns = scriptInsertColumns(getFieldsOnDatabase(connection));
                String values = scriptInsertValues(connection);

                string query = $"insert into {tableName()} ({columns}) values ({values})";
                // Console.WriteLine(query);

                command.CommandText = query;

                rowsAffected += command.ExecuteNonQuery();
            }

            // Close the connection if it was created within this method
            if (shouldCloseConnection)
            {
                connection.Close();
            }
        }

        public Boolean hasResult(String query, NpgsqlConnection connection)
        {
            bool shouldCloseConnection = false;

            // Create a new connection if the provided connection is null
            if (connection == null)
            {
                string connectionString = $"Server=localhost;Port=5432;Database={db};User Id={user};Password={pwd};";
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
                shouldCloseConnection = true;
            }

            using (var command = new NpgsqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return true;
                    }
                }
            }

            // Close the connection if it was created within this method
            if (shouldCloseConnection)
            {
                connection.Close();
            }

            return false;
        }

        public int getInteger(String query, NpgsqlConnection connection)
        {
            bool shouldCloseConnection = false;

            // Create a new connection if the provided connection is null
            if (connection == null)
            {
                string connectionString = $"Server=localhost;Port=5432;Database={db};User Id={user};Password={pwd};";
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
                shouldCloseConnection = true;
            }

            using (var command = new NpgsqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return (int) reader.GetValue(0);
                    }
                }
            }

            // Close the connection if it was created within this method
            if (shouldCloseConnection)
            {
                connection.Close();
            }

            return 0;
        }

        public double getDouble(String query, NpgsqlConnection connection)
        {
            bool shouldCloseConnection = false;

            // Create a new connection if the provided connection is null
            if (connection == null)
            {
                string connectionString = $"Server=localhost;Port=5432;Database={db};User Id={user};Password={pwd};";
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
                shouldCloseConnection = true;
            }

            using (var command = new NpgsqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader.GetDouble(0);
                    }
                }
            }

            // Close the connection if it was created within this method
            if (shouldCloseConnection)
            {
                connection.Close();
            }

            return 0;
        }
    }
}
