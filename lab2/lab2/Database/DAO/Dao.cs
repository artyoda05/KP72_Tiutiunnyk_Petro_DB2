using System.Collections.Generic;

namespace lab2.Database.DAO
{
    public abstract class Dao<T>
    {
        protected DbConnection Dbconnection;
        protected Dao(DbConnection db)
        {
            Dbconnection = db;
        }

        public abstract void Create(T entity);
        public abstract T Get(long id);
        public abstract List<T> Get(int page);
        public abstract void Update(T entity);
        public abstract void Delete(long id);
        public abstract void Clear();
    }
}
