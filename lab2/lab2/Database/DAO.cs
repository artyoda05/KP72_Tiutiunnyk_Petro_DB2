using System.Collections.Generic;

namespace lab2.Database
{
    public abstract class Dao<T>
    {
        protected DBConnection Dbconnection;
        protected Dao(DBConnection db)
        {
            Dbconnection = db;
        }

        public abstract void Create(T entity);
        public abstract T Get(long id);
        public abstract List<T> Get(int page);
        public abstract void Update(T entity);
        public abstract void Delete(long id);
    }
}
