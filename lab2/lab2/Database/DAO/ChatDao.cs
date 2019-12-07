using System;
using System.Collections.Generic;
using System.Text;
using lab2.Models;

namespace lab2.Database.DAO
{
    class ChatDao : Dao<Chat>
    {
        public ChatDao(DbConnection db) : base(db) { }

        public override void Create(Chat entity)
        {
            throw new NotImplementedException();
        }

        public override Chat Get(long id)
        {
            throw new NotImplementedException();
        }

        public override List<Chat> Get(int page)
        {
            throw new NotImplementedException();
        }

        public override void Update(Chat entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
