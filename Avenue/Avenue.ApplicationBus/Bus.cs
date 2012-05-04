using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avenue.ApplicationBus
{
    public class Bus : ICommandSender, IEventPublisher
    {
        #region singleton ctor

        private static Bus instance = new Bus();
        private static object locker = new object();
        private static readonly InternalBus _bus = new InternalBus();

        public static Bus Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new Bus();
                            //load all handlers by default.
                        }
                    }
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        #endregion



        public void Publish<T>(T @event) where T : Event
        {
            _bus.Publish(@event);
        }

        public void Send<T>(T command) where T : Command
        {
            _bus.Send(command);
        }

        public static void PublishEvent<T>(T @event) where T : Event
        {
            ApplicationBus.Bus.instance.Publish(@event);
        }

        public static void SendCommand<T>(T command) where T : Command
        {
            ApplicationBus.Bus.instance.Send(command);

        }

        #region Remove me

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
        #endregion
    }
}
