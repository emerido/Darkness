using System.Collections.Generic;
using SimpleInjector;

namespace Darkness.Event.SimpleInjector
{
    public class EventHandlerResolver : IEventHandlerResolver
    {
        private readonly Container _container;

        public EventHandlerResolver(Container container)
        {
            _container = container;
        }

        public IEnumerable<IEventHandler<TEvent>> Resolve<TEvent>() where TEvent : IEvent
        {
            return _container.GetAllInstances<IEventHandler<TEvent>>();
        }
    }
}