using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using Darkness.Cqrs.Errors;
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
        public void Handle_Command_With_Context_Handler()
        {
            var fakeCommand = new Command();
            var fakeContext = new CommandContext();

            var handler = new Mock<ICommandHandler<Command, CommandContext>>();
            handler.Setup(x => x.Handle(fakeCommand, fakeContext));
            
            _mockHandlerFactory
                .Setup(x => x.Resolve(typeof(ICommandHandler<Command, CommandContext>)))
                .Returns(handler.Object);

            _dispatcher.Handle(fakeCommand, fakeContext);
        }

        [Fact]
        public void Handle_Void_Command_Handler()
        {
            var voidCommand = new Command();
            
            var handler = new Mock<ICommandHandler<Command>>();
            
            handler
                .Setup(x => x.Handle(voidCommand));
            
            _mockHandlerFactory
                .Setup(x => x.Resolve(typeof(ICommandHandler<Command>)))
                .Returns(handler.Object);

            _dispatcher.Handle(voidCommand);
            
            Assert.True(true);
        }
        
        [Fact]
        public async Task Handle_Void_Command_Async_Handler()
        {
            var voidCommand = new Command();
            
            var handler = new Mock<ICommandHandlerAsync<Command>>();
            
            handler
                .Setup(x => x.Handle(voidCommand, _cancellationToken))
                .Returns(Task.CompletedTask);
            
            _mockHandlerFactory
                .Setup(x => x.Resolve(typeof(ICommandHandlerAsync<Command>)))
                .Returns(handler.Object);

            await _dispatcher.HandleAsync(voidCommand, _cancellationToken);
            
            Assert.True(true);
        }
        
        [Fact]
        public async void Handle_Command_With_Context_Async_Handler()
        {
            var fakeCommand = new Command();
            var fakeContext = new CommandContext();

            var handler = new Mock<ICommandHandlerAsync<Command, CommandContext>>();
            handler.Setup(x => x.Handle(fakeCommand, fakeContext, _cancellationToken))
                .Returns(Task.CompletedTask);
            
            _mockHandlerFactory
                .Setup(x => x.Resolve(typeof(ICommandHandlerAsync<Command, CommandContext>)))
                .Returns(handler.Object);

            await _dispatcher.HandleAsync(fakeCommand, fakeContext, _cancellationToken);
        }

        [Fact]
        public void Handle_Not_Existing_Command_Handler()
        {
            Assert.Throws<HandlerNotFound>(() =>
            {
                _dispatcher.Handle(new CommandWithoutHandler());
            });
        }
        
    }

    public class CommandWithoutHandler : ICommand
    {
        
    }
    
    public class Command : ICommand
    {
			
    }

    public class CommandContext
    {
        
    }
    
}