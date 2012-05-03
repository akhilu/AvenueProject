using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avenue.ApplicationBus
{
    public class Bus : ICommandSender, IEventPublisher
    {
        #region singleton ctor

        private static readonly Bus instance = new Bus();

        private Bus()
        {

        }

        public static Bus Instance
        {
            get { return instance; }
        }

        #endregion

        private static readonly InternalBus _bus = new InternalBus();

        public void Publish<T>(T @event) where T : Event
        {
            _bus.Publish(@event);
        }

        public void Send<T>(T command) where T : Command
        {
            _bus.Send(command);
        }

        public void RegisterCommandHandler<C, H>()
            where C : Command
            where H : HandlesCommand<C>
        {
            _bus.RegisterCommandHandler<C, H>();
        }

        public void RegisterEventHandler<E, H>()
            where E : Event
            where H : HandlesEvent<E>
        {
            _bus.RegisterEventHandler<E, H>();
        }

        public void ResetRoutes()
        {
            _bus.ResetRoutes();
        }

        public static void PublishEvent<T>(T @event) where T : Event
        {
            ApplicationBus.Bus.instance.Publish(@event);

        }

        public static void SendCommand<T>(T command) where T : Command
        {
            ApplicationBus.Bus.instance.Send(command);

        }

        public static void RegisterHandlerForCommand<C, H>()
            where C : Command
            where H : HandlesCommand<C>
        {
            ApplicationBus.Bus.instance.RegisterCommandHandler<C, H>();


        }

        public static void RegisterHandlerForEvent<E, H>()
            where E : Event
            where H : HandlesEvent<E>
        {
            ApplicationBus.Bus.instance.RegisterEventHandler<E, H>();
        }

    }
}
