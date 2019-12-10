using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace lab2.Database
{
    class FullTestSearch
    {
        private DbConnection _dbConnection;

        public FullTestSearch(DbConnection dbConnection) =>
            _dbConnection = dbConnection;

        public List<SearchResult> GetFullPhrase(string atr, string table, string phrase)
        {
            List<SearchResult> list = new List<SearchResult>();
            NpgsqlConnection connection = _dbConnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT id, {atr}, ts_headline(\"{atr}\", q) FROM {table}, phraseto_tsquery('{phrase}') AS q WHERE to_tsvector({table}.{atr}) @@ q";
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                SearchResult s = new SearchResult(Convert.ToInt64(reader.GetValue(0).ToString()), reader.GetValue(1).ToString(), reader.GetValue(2).ToString());
                list.Add(s);
            }
            _dbConnection.Close();
            return list;
        }


        public List<SearchResult> GetAllWithIncludedWord(string atr, string table, string phrase)
        {
            List<SearchResult> list = new List<SearchResult>();
            NpgsqlConnection connection = _dbConnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT id, {atr} FROM {table} WHERE (to_tsvector({table}.{atr}) @@ to_tsquery('{phrase}'))";
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                SearchResult s = new SearchResult(Convert.ToInt64(reader.GetValue(0).ToString()), reader.GetValue(1).ToString(), null);
                list.Add(s);
            }
            _dbConnection.Close();
            return list;
        }
    }
}
