using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;

namespace Darkness.Cqrs.Tests
{
    
    public class CommandBusTest
    {
        
        private readonly CancellationToken _cancellationToken;
        private readonly Mock<ICommandHandlerFactory> _mockHandlerFactory;
        private readonly CommandBus _dispatcher;

        public CommandBusTest()
        {
            _cancellationToken = CancellationToken.None;
            _mockHandlerFactory = new Mock<ICommandHandlerFactory>();
            _dispatcher = new CommandBus(_mockHandlerFactory.Object);
        }

        
        [Fact]
        public async void HandlerWithResultCommand()
        {
            
            var fakeCommand = new NonVoidCommand();
            var fakeResult = new CommandResult();

            var handler = new Mock<ICommandHandler<NonVoidCommand, CommandResult>>();
                handler.Setup(x => x.Handle(fakeCommand, _cancellationToken))
                .ReturnsAsync(fakeResult);
            
            _mockHandlerFactory
                .Setup(x => x.CreateHandler(typeof(ICommandHandler<NonVoidCommand, CommandResult>)))
                .Returns(handler.Object);

            var result = await _dispatcher.Handle(fakeCommand, _cancellationToken);
            
            Assert.Equal(result, fakeResult);
        }

        [Fact]
        public async void HandleVoidCommand()
        {
            var voidCommand = new VoidCommand();
            
            var handler = new Mock<ICommandHandler<VoidCommand>>();
            
            handler
                .Setup(x => x.Handle(voidCommand, _cancellationToken))
                .Returns(Task.CompletedTask);
            
            _mockHandlerFactory
                .Setup(x => x.CreateHandler(typeof(ICommandHandler<VoidCommand>)))
                .Returns(handler.Object);

            await _dispatcher.Handle(voidCommand, _cancellationToken);
            
            Assert.True(true);
        }
        
    }
    
    
    public class VoidCommand : ICommand
    {
			
    }

    public class NonVoidCommand : ICommand<CommandResult>
    {
			
    }

    public class CommandResult
    {
        
    }
    
}