using System;
using System.Threading;
using Darkness.Cqrs.Errors;
using Darkness.Cqrs.Simple;
using Moq;
using Xunit;

namespace Darkness.Cqrs.Tests
{
    
    public class QueryBusTest
    {
        
        private readonly CancellationToken _cancellationToken;
        private readonly Mock<IQueryHandlerResolver> _mockHandlerFactory;
        private readonly QueryBus _dispatcher;

        public QueryBusTest()
        {
            _cancellationToken = CancellationToken.None;
            _mockHandlerFactory = new Mock<IQueryHandlerResolver>();
            _dispatcher = new QueryBus(_mockHandlerFactory.Object);
        }

        [Fact]
        public async void Handle_Async_Query()
        {
            var fakeQuery = new Query();
            var fakeModel = new Model();
            
            var handler = new Mock<IQueryHandlerAsync<Query, Model>>();
            
            handler
                .Setup(x => x.Handle(fakeQuery, _cancellationToken))
                .ReturnsAsync(fakeModel);
            
            _mockHandlerFactory
                .Setup(x => x.Resolve(typeof(IQueryHandlerAsync<Query, Model>)))
                .Returns(handler.Object);
            
            var result = await _dispatcher.AskAsync(fakeQuery, _cancellationToken);
            
            Assert.Equal(result, fakeModel);
        }
        
        [Fact]
        public void Handle_Query()
        {
            var fakeQuery = new Query();
            var fakeModel = new Model();
            
            var handler = new Mock<IQueryHandler<Query, Model>>();
            
            handler
                .Setup(x => x.Handle(fakeQuery))
                .Returns(fakeModel);
            
            _mockHandlerFactory
                .Setup(x => x.Resolve(typeof(IQueryHandler<Query, Model>)))
                .Returns(handler.Object);
            
            var result = _dispatcher.Ask(fakeQuery);
            
            Assert.Equal(result, fakeModel);
        }
        
        [Fact]
        public void Handle_Query_With_Context()
        {
            var fakeQuery = new Query();
            var fakeModel = new Model();
            var fakeContext = new QueryContext();
            
            var handler = new Mock<IQueryHandler<Query, Model, QueryContext>>();
            
            handler
                .Setup(x => x.Handle(fakeQuery, fakeContext))
                .Returns(fakeModel);
            
            _mockHandlerFactory
                .Setup(x => x.Resolve(typeof(IQueryHandler<Query, Model, QueryContext>)))
                .Returns(handler.Object);
            
            var result = _dispatcher.Ask(fakeQuery, fakeContext);
            
            Assert.Equal(result, fakeModel);
        }
        
        [Fact]
        public async void Handle_Async_Query_With_Context()
        {
            var fakeQuery = new Query();
            var fakeModel = new Model();
            var fakeContext = new QueryContext();
            
            var handler = new Mock<IQueryHandlerAsync<Query, Model, QueryContext>>();
            
            handler
                .Setup(x => x.Handle(fakeQuery, fakeContext, _cancellationToken))
                .ReturnsAsync(fakeModel);
            
            _mockHandlerFactory
                .Setup(x => x.Resolve(typeof(IQueryHandlerAsync<Query, Model, QueryContext>)))
                .Returns(handler.Object);
            
            var result = await _dispatcher.AskAsync(fakeQuery, fakeContext, _cancellationToken);
            
            Assert.Equal(result, fakeModel);
        }

        [Fact]
        public void Handle_Not_Existing_Query_Handler()
        {
            Assert.Throws<HandlerNotFound>(() =>
            {
                var t = _dispatcher.Ask(new QueryWithoutHandler());
            });
        }

        [Fact]
        public void Handle_Exception_In_Query_Handler()
        {
            var query = new WrongQuery();
            var handler = new Mock<IQueryHandler<WrongQuery, Model>>();
            
            handler
                .Setup(x => x.Handle(query))
                .Throws<ArgumentException>()
                ;
            
            _mockHandlerFactory
                .Setup(x => x.Resolve(typeof(IQueryHandler<WrongQuery, Model>)))
                .Returns(handler.Object)
                ;
            
            Assert.Throws<ArgumentException>(() => { var t = _dispatcher.Ask(query); });
        }
        
    }

    public class QueryWithoutHandler : IQuery<Model>
    {
        
    }
    
    public class Query : IQuery<Model>
    {
			
    }

    public class Model
    {
			
    }

    public class QueryContext
    {
        
    }

    public class WrongQuery : IQuery<Model>
    {
        
    }
    
}