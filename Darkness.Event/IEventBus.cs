using System.Threading.Tasks;

namespace Darkness.Event
{
    public interface IEventBus
    {

        void Emit<TEvent>(TEvent @event) where TEvent : IEvent;

    }
}