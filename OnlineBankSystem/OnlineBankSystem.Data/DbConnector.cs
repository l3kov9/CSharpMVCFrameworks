namespace OnlineBankSystem.Data
{
    using Common.SqlServer;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public abstract class DbConnector : IDbConnector
    {
        public DbConnector()
            : this(ServerConnection.ConnectionString)
        {
        }

        public DbConnector(string connectionString)
        {
            this.Connection = new SqlConnection(connectionString);
            this.Connection.Open();
        }

        protected SqlConnection Connection { get; private set; }

        protected SqlDataReader ExecuteReader(string commandText, IDictionary<string, object> parameters = null)
        {
            var command = this.PrepareCommand(commandText, parameters);
            return command.ExecuteReader();
        }

        protected object ExecuteScalar(string commandText, IDictionary<string, object> parameters = null)
        {
            var command = this.PrepareCommand(commandText, parameters);
            return command.ExecuteScalar();
        }

        protected int ExecuteNonQuery(string commandText, IDictionary<string, object> parameters = null)
        {
            var command = this.PrepareCommand(commandText, parameters);
            return command.ExecuteNonQuery();
        }

        private SqlCommand PrepareCommand(string commandText, IDictionary<string, object> parameters = null)
        {
            var command = this.Connection.CreateCommand();
            command.CommandText = commandText;
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
            }

            return command;
        }

        public void Dispose()
        {
            this.Connection.Close();
        }
    }
}
