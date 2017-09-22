using System.Collections.Generic;

namespace Darkness.Event
{
    public interface IEventHandlerResolver
    {

        IEnumerable<IEventHandler<TEvent>> Resolve<TEvent>() where TEvent : IEvent;

    }
}