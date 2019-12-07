using System.Collections.Generic;
using lab2.Models;

namespace lab2.Database.DAO
{
    internal class UserDao : Dao<User>
    {
        public UserDao(DbConnection db) : base(db) { }

        public override void Create(User entity)
        {
            throw new System.NotImplementedException();
        }

        public override User Get(long id)
        {
            throw new System.NotImplementedException();
        }

        public override List<User> Get(int page)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(User entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Delete(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}