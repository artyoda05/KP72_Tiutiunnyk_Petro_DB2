using System;
using System.Collections.Generic;
using System.Text;
using lab2.Models;
using Npgsql;
using NpgsqlTypes;

namespace lab2.Database.DAO
{
    class MessageDao : Dao<Message>
    {
        private readonly Dao<User> _userDao;
        private readonly Dao<Chat> _chatDao;
        public MessageDao(DbConnection db, Dao<User> userDao, Dao<Chat> chatDao)
            : base(db)
        {
            _userDao = userDao;
            _chatDao = chatDao;
        }

        public override void Create(Message entity)
        {
            var connection = Dbconnection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
                "INSERT INTO public.message (chat_id, user_id, text, date) " +
                "VALUES (:chat_id, :user_id, :text, :date)";
            command.Parameters.Add(new NpgsqlParameter("chat_id", entity.Chat.Id));
            command.Parameters.Add(new NpgsqlParameter("user_id", entity.User.Id));
            command.Parameters.Add(new NpgsqlParameter("text", entity.Text));
            command.Parameters.Add(new NpgsqlParameter("date", NpgsqlDateTime.Now));
            command.ExecuteNonQuery();
            Dbconnection.Close();
        }

        public override Message Get(long id)
        {
            var connection = Dbconnection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
                "SELECT * FROM public.message WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            var reader = command.ExecuteReader();
            Message message = null;
            if (reader.Read())
                message = new Message(reader.GetInt64(0),
                    new User(reader.GetInt64(2)),
                    new Chat(reader.GetInt64(1)),
                    reader.GetString(3),
                    reader.GetTimeStamp(4).ToDateTime());
            Dbconnection.Close();
            if (!(message is null))
            {
                message.User = _userDao.Get(message.User.Id);
                message.Chat = _chatDao.Get(message.Chat.Id);
            }
            return message;
        }

        public override List<Message> Get(int page)
        {
            var connection = Dbconnection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
                "SELECT * FROM public.message LIMIT 10 OFFSET :offset";
            command.Parameters.Add(new NpgsqlParameter("offset", page * 10));
            var reader = command.ExecuteReader();
            var messages = new List<Message>();
            while (reader.Read())
                messages.Add(new Message(reader.GetInt64(0),
                    new User(reader.GetInt64(2)), 
                    new Chat(reader.GetInt64(1)),
                    reader.GetString(3),
                    reader.GetTimeStamp(4).ToDateTime()));
            Dbconnection.Close();
            foreach (var message in messages)
            {
                message.User = _userDao.Get(message.User.Id);
                message.Chat = _chatDao.Get(message.Chat.Id);
            }
            return messages;
        }

        public override void Update(Message entity)
        {
            var connection = Dbconnection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
                "UPDATE public.message SET text = :text WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", entity.Id));
            command.Parameters.Add(new NpgsqlParameter("text", entity.Text));
            command.ExecuteNonQuery();
            Dbconnection.Close();
        }

        public override void Delete(long id)
        {
            var connection = Dbconnection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM public.message WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            command.ExecuteNonQuery();
            Dbconnection.Close();
        }

        public override void Clear()
        {
            var connection = Dbconnection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "TRUNCATE TABLE public.message RESTART IDENTITY";
            command.ExecuteNonQuery();
            Dbconnection.Close();
        }
    }
}
