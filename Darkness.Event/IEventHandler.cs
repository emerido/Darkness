using System.Threading.Tasks;

namespace Darkness.Event
{
    
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {

        void Handle(TEvent @event);

    }
    
}