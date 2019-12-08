using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ConsoleTableExt;
using lab2.Database.DAO;
using lab2.Models;

namespace lab2.Views.CrudViews
{
    class UserChatView : CrudView<UserChat>
    {
        private readonly Dao<User> _userDao;
        private readonly Dao<Chat> _chatDao;

        public UserChatView(Dao<User> userDao, Dao<Chat> chatDao) 
            : base("User and chat relations")
        {
            _userDao = userDao;
            _chatDao = chatDao;
        }

        public override UserChat Create()
        {
            Console.WriteLine("\n\rInput user id:");
            var user = GetUser();
            Console.WriteLine("Input chat id:");
            var chat = GetChat();
            Console.WriteLine("Input user is admin:");
            var isAdmin = GetBool();
            return new UserChat(0, user, chat, isAdmin);
        }

        public override void ShowReadResult(UserChat data)
        {
            Console.WriteLine("Result:");
            ConsoleTableBuilder.From(DataToDataTable(new List<UserChat> { data }))
                .WithFormat(ConsoleTableBuilderFormat.Alternative)
                .ExportAndWriteLine();
            Console.ReadKey();
        }

        public override UserChat Update(UserChat entity)
        {
            Console.WriteLine($"\n\rInput if user is admin. Old value: {entity.IsAdmin}");
            entity.IsAdmin = GetBool();
            return entity;
        }

        protected override DataTable DataToDataTable(List<UserChat> data)
        {
            var dataTable = new DataTable("Users");
            dataTable.Columns.Add(new DataColumn("Id", typeof(long)));
            dataTable.Columns.Add(new DataColumn("User name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Chat Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("IsAdmin", typeof(bool)));
            if (data.Count == 0)
            {
                var row = dataTable.NewRow();
                row.ItemArray = new object[] { -1, "Empty", "Empty", false };
                dataTable.Rows.Add(row);
            }
            else
                foreach (var el in data)
                {
                    var row = dataTable.NewRow();
                    row.ItemArray = new object[]
                    {
                        el.Id,
                        el.User.Login,
                        el.Chat.Name,
                        el.IsAdmin
                    };
                    dataTable.Rows.Add(row);
                }

            return dataTable;
        }

        private User GetUser()
        {
            while (true)
            {
                var user = _userDao.Get(GetNum());
                if (user is null)
                    Console.WriteLine("No such user!");
                else
                    return user;
            }
        }
        private Chat GetChat()
        {
            while (true)
            {
                var chat = _chatDao.Get(GetNum());
                if (chat is null)
                    Console.WriteLine("No such chat!");
                else
                    return chat;
            }
        }

        private static long GetNum()
        {
            long number;
            while (!long.TryParse(Console.ReadLine(), out number)
                   || number <= 0)
                Console.WriteLine("Wrong input!");
            return number;
        }

        private static bool GetBool()
        {
            bool boolean;
            while (!bool.TryParse(Console.ReadLine(), out boolean))
                Console.WriteLine("Wrong input!");
            return boolean;
        }
    }
}
