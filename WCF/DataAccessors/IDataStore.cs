using System.Collections.Generic;

namespace abrie.netWeb.WCF.DataAccessors
{
    public interface IDataStore
    {
        List<T> GetAll<T>();
        T Get<T>(string id);
        void Store<T>(T entity);
        void Store<T>(T entity, string id);
        void Delete<T>(T entity);
        void Delete<T>(string id);
    }
}
