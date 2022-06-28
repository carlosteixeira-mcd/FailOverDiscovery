
using Raven.Client.Documents;

namespace Failover.Services
{
    public static class DataBaseService
    {
        private static readonly Lazy<IDocumentStore> LazyStore =
            new Lazy<IDocumentStore>(() =>
            {
                IDocumentStore store = new DocumentStore
                {
                    Urls = new[] { "http://127.0.0.1:8090/" },
                    Database = "FailOverTestDataBase"
                };

                store.Initialize();
                return store;
            });

        public static IDocumentStore Store => LazyStore.Value;
    }
}
