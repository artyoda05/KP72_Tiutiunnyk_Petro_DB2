using System;
using System.Collections.Generic;
using System.Text;
using lab2.Models;

namespace lab2.Database.DAO
{
    class UserChatDao : Dao<UserChat>
    {
        public UserChatDao(DbConnection db) : base(db) { }

        public override void Create(UserChat entity)
        {
            throw new NotImplementedException();
        }

        public override UserChat Get(long id)
        {
            throw new NotImplementedException();
        }

        public override List<UserChat> Get(int page)
        {
            throw new NotImplementedException();
        }

        public override void Update(UserChat entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
