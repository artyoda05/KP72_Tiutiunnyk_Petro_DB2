using System;
using lab2.Database;
using lab2.Database.DAO;
using lab2.Models;
using lab2.Views;

namespace lab2.Controllers
{
    public class Controller
    {
        private Dao<User> _userDao;
        private Dao<Chat> _chatDao;
        private Dao<UserChat> _userChatDao;
        private Dao<Message> _messageDao;

        public Controller(DbConnection dbConnection)
        {
            _userDao = new UserDao(dbConnection);
            _chatDao = new ChatDao(dbConnection);
            _userChatDao =
                new UserChatDao(dbConnection, _userDao, _chatDao);
            _messageDao =
                new MessageDao(dbConnection, _userDao, _chatDao);
        }

        public void Begin()
        {
            while (true)
            {
                var menuCom = MenuView.ShowMain();
                if (menuCom == MenuCommands.Exit)
                    break;
                if (menuCom == MenuCommands.Crud)
                    BeginCrud();
                if (menuCom == MenuCommands.Random)
                    throw new NotImplementedException();
                if (menuCom == MenuCommands.FullTextSearch)
                    throw new NotImplementedException();
            }
        }

        private void BeginCrud()
        {
            while (true)
            {
                var entity = MenuView.ShowEntities();
                if (entity == Entities.None)
                    break;

            }
        }
    }
}