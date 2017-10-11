using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using Darkness.Cqrs.Simple;

namespace Darkness.Cqrs.Tests
{
    
    public class CommandBusTest
    {
        
        private readonly CancellationToken _cancellationToken;
        private readonly Mock<ICommandHandlerResolver> _mockHandlerFactory;
        private readonly CommandBus _dispatcher;

        public CommandBusTest()
        {
            _cancellationToken = CancellationToken.None;
            _mockHandlerFactory = new Mock<ICommandHandlerResolver>();
            _dispatcher = new CommandBus(_mockHandlerFactory.Object);
        }

        
        [Fact]
        public void Handle_Command_With_Result_Handler()
        {
            
            var fakeCommand = new NonVoidCommand();
            var fakeResult = new CommandResult();

            var handler = new Mock<ICommandHandler<NonVoidCommand, CommandResult>>();
                handler.Setup(x => x.Handle(fakeCommand))
                .Returns(fakeResult);
            
            _mockHandlerFactory
                .Setup(x => x.Resolve(typeof(ICommandHandler<NonVoidCommand, CommandResult>)))
                .Returns(handler.Object);

            var result = _dispatcher.Handle(fakeCommand);
            
            Assert.Equal(result, fakeResult);
        }

        [Fact]
        public void Handle_Void_Command_Handler()
        {
            var voidCommand = new VoidCommand();
            
            var handler = new Mock<ICommandHandler<VoidCommand>>();
            
            handler
                .Setup(x => x.Handle(voidCommand));
            
            _mockHandlerFactory
                .Setup(x => x.Resolve(typeof(ICommandHandler<VoidCommand>)))
                .Returns(handler.Object);

            _dispatcher.Handle(voidCommand);
            
            Assert.True(true);
        }
        
        [Fact]
        public async Task Handle_Void_Command_Async_Handler()
        {
            var voidCommand = new VoidCommand();
            
            var handler = new Mock<ICommandHandlerAsync<VoidCommand>>();
            
            handler
                .Setup(x => x.Handle(voidCommand, _cancellationToken))
                .Returns(Task.CompletedTask);
            
            _mockHandlerFactory
                .Setup(x => x.Resolve(typeof(ICommandHandlerAsync<VoidCommand>)))
                .Returns(handler.Object);

            await _dispatcher.HandleAsync(voidCommand, _cancellationToken);
            
            Assert.True(true);
        }
        
        [Fact]
        public async void Handle_Command_With_Result_Async_Handler()
        {
            
            var fakeCommand = new NonVoidCommand();
            var fakeResult = new CommandResult();

            var handler = new Mock<ICommandHandlerAsync<NonVoidCommand, CommandResult>>();
            handler.Setup(x => x.Handle(fakeCommand, _cancellationToken))
                .ReturnsAsync(fakeResult);
            
            _mockHandlerFactory
                .Setup(x => x.Resolve(typeof(ICommandHandlerAsync<NonVoidCommand, CommandResult>)))
                .Returns(handler.Object);

            var result = await _dispatcher.HandleAsync(fakeCommand, _cancellationToken);
            
            Assert.Equal(result, fakeResult);
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