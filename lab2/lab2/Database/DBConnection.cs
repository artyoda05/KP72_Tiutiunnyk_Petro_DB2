using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace lab2.Database
{
    public class DBConnection
    {
        private readonly NpgsqlConnection _connection;

        public DBConnection(string connectString)
        {
            _connection = new NpgsqlConnection(connectString);
        }

        public NpgsqlConnection Open()
        {
            _connection.Open();
            return _connection;
        }

        public void Close()
        {
            _connection.Close();
        }
    }
}
