using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Darkness.Event.Simple;
using Moq;
using Xunit;

namespace Darkness.Event.Tests
{
    public class EventBusTest
    {

        private readonly IEnumerable<IEventHandler<SimpleEvent>> _handlers;
        private readonly Mock<IEventHandlerResolver> _resolver;
        private readonly IEventBus _dispatcher;


        public EventBusTest()
        {
            _handlers = new []
            {
                new SimpleEventHandler(1), 
                new SimpleEventHandler(2), 
                new SimpleEventHandler(3), 
                new SimpleEventHandler(4), 
            };
            
            _resolver = new Mock<IEventHandlerResolver>();
            _resolver.Setup(x => x.Resolve<SimpleEvent>()).Returns(_handlers);

            _dispatcher = new EventBus(_resolver.Object);
        }

        [Fact]
        public void EmitTest()
        {
            _dispatcher.Emit(new SimpleEvent());

        }
    }


    public class SimpleEvent : IEvent
    {
        
    }
    
    public class SimpleEventHandler : IEventHandler<SimpleEvent>
    {
        private readonly int _order;

        public SimpleEventHandler(int order)
        {
            _order = order;
        }

        public void Handle(SimpleEvent @event)
        {
            Console.WriteLine($"I'm a {_order} in the emit");
        }
    }
}