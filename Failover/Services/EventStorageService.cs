using Failover.Model;

namespace Failover.Services
{
    public class EventStorageService
    {
        private readonly RavenDbPolicy _policy;
        public EventStorageService()
        {
            _policy = new RavenDbPolicy();
        }
        public void StoreEvent(BaseEvent baseEvent)
        {
            Console.WriteLine("First attempt...");
            _policy.PoliciesWrap.Execute(() =>
            {
                using var session = DataBaseService.Store.OpenSession();
                session.Store(baseEvent);
                session.SaveChanges();
                Console.WriteLine("New event stored!");
            });
        }
    }
}
