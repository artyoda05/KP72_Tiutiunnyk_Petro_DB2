using System;
using System.Collections.Generic;
using System.Text;
using lab2.Models;
using Npgsql;

namespace lab2.Database.DAO
{
    class UserChatDao : Dao<UserChat>
    {
        private readonly Dao<User> _userDao;
        private readonly Dao<Chat> _chatDao;

        public UserChatDao(DbConnection db, Dao<User> userDao, Dao<Chat> chatDao)
            : base(db)
        {
            _userDao = userDao;
            _chatDao = chatDao;
        }

        public override void Create(UserChat entity)
        {
            var connection = Dbconnection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
                "INSERT INTO public.user_chat (user_id, chat_id, isAdmin) " +
                "VALUES (:user_id, :chat_id, :isAdmin)";
            command.Parameters.Add(new NpgsqlParameter("user_id", entity.User.Id));
            command.Parameters.Add(new NpgsqlParameter("chat_id", entity.Chat.Id));
            command.Parameters.Add(new NpgsqlParameter("text", entity.IsAdmin));
            command.ExecuteNonQuery();
            Dbconnection.Close();
        }

        public override UserChat Get(long id)
        {
            var connection = Dbconnection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
                "SELECT * FROM public.user_chat WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            var reader = command.ExecuteReader();
            UserChat userChat = null;
            if (reader.Read())
                userChat = new UserChat(reader.GetInt64(0),
                    _userDao.Get(reader.GetInt64(1)),
                    _chatDao.Get(reader.GetInt64(2)),
                    reader.GetBoolean(3));
            return userChat;
        }

        public override List<UserChat> Get(int page)
        {
            var connection = Dbconnection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
                "SELECT * FROM public.user_chat LIMIT 10 OFFSET :offset";
            command.Parameters.Add(new NpgsqlParameter("offset", page * 10));
            var reader = command.ExecuteReader();
            var userChats = new List<UserChat>();
            while (reader.Read())
                userChats.Add(new UserChat(reader.GetInt64(0),
                    _userDao.Get(reader.GetInt64(1)),
                    _chatDao.Get(reader.GetInt64(2)),
                    reader.GetBoolean(3)));
            return userChats;
        }

        public override void Update(UserChat entity)
        {
            var connection = Dbconnection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
                "UPDATE public.user_chat SET isAdmin = :isAdmin WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", entity.Id));
            command.Parameters.Add(new NpgsqlParameter("isAdmin", entity.IsAdmin));
            command.ExecuteNonQuery();
            Dbconnection.Close();
        }

        public override void Delete(long id)
        {
            var connection = Dbconnection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM public.user_chat WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            command.ExecuteNonQuery();
            Dbconnection.Close();
        }
    }
}
