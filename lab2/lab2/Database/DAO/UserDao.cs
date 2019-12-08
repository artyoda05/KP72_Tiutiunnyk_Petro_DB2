using System.Collections.Generic;
using lab2.Models;
using Npgsql;

namespace lab2.Database.DAO
{
    internal class UserDao : Dao<User>
    {
        public UserDao(DbConnection db) : base(db) { }

        public override void Create(User entity)
        {
            var connection = Dbconnection.Open();
            var command = connection.CreateCommand();
            command.CommandText = 
                "INSERT INTO public.user (login, name, bio) VALUES (:login, :name, :bio)";
            command.Parameters.Add(new NpgsqlParameter("login", entity.Login));
            command.Parameters.Add(new NpgsqlParameter("name", entity.Name));
            command.Parameters.Add(new NpgsqlParameter("bio", entity.Bio));
            command.ExecuteNonQuery();
            Dbconnection.Close();
        }

        public override User Get(long id)
        {
            var connection = Dbconnection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
                "SELECT * FROM public.user WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            var reader = command.ExecuteReader();
            User user = null;
            if (reader.Read())
                user = new User(reader.GetInt64(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.IsDBNull(3)
                                    ? null
                                    : reader.GetString(3));
            Dbconnection.Close();
            return user;
        }

        public override List<User> Get(int page)
        {
            var connection = Dbconnection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM public.user LIMIT 10 OFFSET :offset";
            command.Parameters.Add(new NpgsqlParameter("offset", page * 10));
            var reader = command.ExecuteReader();
            var users = new List<User>();
            while (reader.Read())
                users.Add(new User(reader.GetInt64(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.IsDBNull(3)
                        ? null
                        : reader.GetString(3)));
            Dbconnection.Close();
            return users;
        }

        public override void Update(User entity)
        {
            var connection = Dbconnection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
                "UPDATE public.user SET login = :login, name = :name, bio = :bio WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", entity.Id));
            command.Parameters.Add(new NpgsqlParameter("login", entity.Login));
            command.Parameters.Add(new NpgsqlParameter("name", entity.Name));
            command.Parameters.Add(new NpgsqlParameter("bio", entity.Bio));
            command.ExecuteNonQuery();
            Dbconnection.Close();
        }

        public override void Delete(long id)
        {
            var connection = Dbconnection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM public.user WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            command.ExecuteNonQuery();
            Dbconnection.Close();
        }
    }
}