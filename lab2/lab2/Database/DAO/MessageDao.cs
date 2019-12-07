using System;
using System.Collections.Generic;
using System.Text;
using lab2.Models;

namespace lab2.Database.DAO
{
    class MessageDao : Dao<Message>
    {
        public MessageDao(DbConnection db) : base(db) { }

        public override void Create(Message entity)
        {
            throw new NotImplementedException();
        }

        public override Message Get(long id)
        {
            throw new NotImplementedException();
        }

        public override List<Message> Get(int page)
        {
            throw new NotImplementedException();
        }

        public override void Update(Message entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
