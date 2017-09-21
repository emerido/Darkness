using System;
using System.Threading;
using Moq;
using Xunit;

namespace Darkness.Cqrs.Tests
{
    
    public class QueryBusTest
    {
        
        private readonly CancellationToken _cancellationToken;
        private readonly Mock<IQueryHandlerFactory> _mockHandlerFactory;
        private readonly QueryBus _dispatcher;

        public QueryBusTest()
        {
            _cancellationToken = CancellationToken.None;
            _mockHandlerFactory = new Mock<IQueryHandlerFactory>();
            _dispatcher = new QueryBus(_mockHandlerFactory.Object);
        }

        [Fact]
        public async void HandlerResolveAndCall()
        {
            var fakeQuery = new FakeQuery();
            var fakeModel = new FakeModel();
            
            var handler = new Mock<IQueryHandler<FakeQuery, FakeModel>>();
            
            handler
                .Setup(x => x.Handle(fakeQuery, _cancellationToken))
                .ReturnsAsync(fakeModel);
            
            _mockHandlerFactory
                .Setup(x => x.CreateHandler(typeof(IQueryHandler<FakeQuery, FakeModel>)))
                .Returns(handler.Object);
            
            var result = await _dispatcher.Ask(fakeQuery, _cancellationToken);
            
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