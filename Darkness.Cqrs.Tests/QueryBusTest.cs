using System;
using System.Threading;
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
            var fakeQuery = new FakeQuery();
            var fakeModel = new FakeModel();
            
            var handler = new Mock<IQueryHandlerAsync<FakeQuery, FakeModel>>();
            
            handler
                .Setup(x => x.Handle(fakeQuery, _cancellationToken))
                .ReturnsAsync(fakeModel);
            
            _mockHandlerFactory
                .Setup(x => x.Resolve(typeof(IQueryHandlerAsync<FakeQuery, FakeModel>)))
                .Returns(handler.Object);
            
            var result = await _dispatcher.AskAsync(fakeQuery, _cancellationToken);
            
            Assert.Equal(result, fakeModel);
        }
        
        [Fact]
        public void Handle_Query()
        {
            var fakeQuery = new FakeQuery();
            var fakeModel = new FakeModel();
            
            var handler = new Mock<IQueryHandler<FakeQuery, FakeModel>>();
            
            handler
                .Setup(x => x.Handle(fakeQuery))
                .Returns(fakeModel);
            
            _mockHandlerFactory
                .Setup(x => x.Resolve(typeof(IQueryHandler<FakeQuery, FakeModel>)))
                .Returns(handler.Object);
            
            var result = _dispatcher.Ask(fakeQuery);
            
            Assert.Equal(result, fakeModel);
        }
    }
    
    public class FakeQuery : IQuery<FakeModel>
    {
			
    }

    public class FakeModel
    {
			
    }
    
    
    
    
}