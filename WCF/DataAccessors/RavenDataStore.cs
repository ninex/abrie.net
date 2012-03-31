using System.Collections.Generic;
using System.Linq;
//using Raven.Client.Embedded;

namespace abrie.netWeb.WCF.DataAccessors
{
    public class RavenDataStore //: IDataStore
    {
        /*
        private EmbeddableDocumentStore documentStore;

        public RavenDataStore()
        {
            documentStore = new EmbeddableDocumentStore
            {
                ConnectionStringName="RavenDB",
                Configuration =
                {
                    DefaultStorageTypeName = "munin"
                }
            };
            documentStore.Initialize();
        }

        #region IDataStore Members

        public List<T> GetAll<T>()
        {
            using (var session = documentStore.OpenSession())
            {
                return (List<T>)session.Query<T>().ToList();
            }
        }

        public T Get<T>(string id)
        {
            using (var session = documentStore.OpenSession())
            {
                return session.Load<T>(id);
            }
        }

        public void Store<T>(T entity)
        {
            using (var session = documentStore.OpenSession())
            {
                session.Store(entity);
                session.SaveChanges();
            }
        }
        public void Store<T>(T entity, string id)
        {
            using (var session = documentStore.OpenSession())
            {
                session.Store(entity, id);
                session.SaveChanges();
            }
        }

        public void Delete<T>(T entity)
        {
            using (var session = documentStore.OpenSession())
            {
                session.Delete(entity);
                session.SaveChanges();
            }
        }

        public void Delete<T>(string id)
        {
            using (var session = documentStore.OpenSession())
            {
                var entity = session.Load<T>(id);
                if (entity != null)
                {
                    session.Delete(entity);
                    session.SaveChanges();
                }
            }
        }

        #endregion*/
    }
}