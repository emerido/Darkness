namespace Darkness.Event
{
    
    public interface IEventHandler<in TEvent>
    {

        void Handle(TEvent @event);

    }
    
}