using System;
using lab2.Database;
using lab2.Database.DAO;
using lab2.Models;
using lab2.Views;
using lab2.Views.CrudViews;

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
                if (entity == Entities.User)
                    ExecuteCrud(new UserView(), _userDao);
                if (entity == Entities.Chat)
                    ExecuteCrud(new ChatView(), _chatDao);
                if (entity == Entities.Message)
                    ExecuteCrud(new MessageView(_userDao, _chatDao), _messageDao);
                if (entity == Entities.UserChat)
                    ExecuteCrud(new UserChatView(_userDao, _chatDao),  _userChatDao);
            }
        }

        private void ExecuteCrud<T>(CrudView<T> view, Dao<T> dao)
        {
            var page = 0;
            while (true)
            {
                var com = view.Begin(dao.Get(page), page);
                if (com == CrudOperations.None)
                    break;
                if (com == CrudOperations.PageLeft && page > 0)
                    page--;
                if (com == CrudOperations.PageRight)
                    page++;
                try
                {
                    if (com == CrudOperations.Create)
                        dao.Create(view.Create());
                    if (com == CrudOperations.Read)
                        view.ShowReadResult(dao.Get(view.Read()));
                    if (com == CrudOperations.Update)
                        dao.Update(view.Update(dao.Get(view.Read())));
                    if (com == CrudOperations.Delete)
                        dao.Delete(view.Read());
                    if (com == CrudOperations.Create || com == CrudOperations.Delete
                                                     || com == CrudOperations.Update)
                        view.OperationStatusOutput(true);
                }
                catch 
                {
                    view.OperationStatusOutput(false);
                }
            }
        }
    }
}