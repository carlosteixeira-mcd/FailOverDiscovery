using Failover.Model;
using Failover.Services;


List<BaseEvent> events = new List<BaseEvent>();

events.Add(new BaseEvent { Name = "SaleStart", EventStart = DateTime.Now.Date });
events.Add(new BaseEvent { Name = "Order", EventStart = DateTime.Now.Date });
events.Add(new BaseEvent { Name = "SaleEnd", EventStart = DateTime.Now.Date });

while (true)
{
    foreach (var ev in events)
    {
        var eventStorageService = new EventStorageService();
        eventStorageService.StoreEvent(ev);
    }
    Thread.Sleep(5000);
}