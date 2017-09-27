using System;

namespace Darkness.Event.Simple
{
    public class EventBus : IEventBus
    {
        private readonly IEventHandlerResolver _resolver;

        public EventBus(IEventHandlerResolver resolver)
        {
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public void Emit<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (@event == null) 
                throw new ArgumentNullException(nameof(@event));
            
            foreach (var handler in _resolver.Resolve<TEvent>())
            {
                handler.Handle(@event);
            }
        }
    }
}