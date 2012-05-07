using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Avenue.ApplicationBus
{
    public class Bus : ICommandSender, IEventPublisher
    {
        #region singleton ctor

        private static Bus instance;
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
                            WireUpAppDomainHandlers();
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

        public void RegisterHandler(Type messageType, Type handlerType)
        {
            _bus.RegisterHandler(messageType, handlerType);

        }

        public static void PublishEvent<T>(T @event) where T : Event
        {
            ApplicationBus.Bus.Instance.Publish(@event);
        }

        public static void SendCommand<T>(T command) where T : Command
        {
            ApplicationBus.Bus.Instance.Send(command);

        }

        

        private static void WireUpAppDomainHandlers()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                
                try
                {
                    ScanTypesForHandlers(assembly.GetTypes());

                }
                catch (ReflectionTypeLoadException exception)
                {
                    var types = exception.Types;
                }

                
            }

        }

        private static void ScanTypesForHandlers(Type [] types)
        {
            foreach (var type in types)
            {
                
                if (type == null || type.IsInterface || type.IsAbstract || type.IsNestedPrivate)
				    continue;

                if (type.GetInterface(typeof(HandlesCommand<>).Name) != null)
                {
                    var handlesTypeOf = type.GetInterfaces()[0].GetGenericArguments()[0];

                    ApplicationBus.Bus.Instance.RegisterHandler(handlesTypeOf, type);

                }

                if (type.GetInterface(typeof(HandlesEvent<>).Name) != null)
                {
                    var handlesTypeOf = type.GetInterfaces()[0].GetGenericArguments()[0];

                    ApplicationBus.Bus.Instance.RegisterHandler(handlesTypeOf, type);

                }

            }

        }

        //#region Remove me

        //public void RegisterCommandHandler<C, H>()
        //    where C : Command
        //    where H : HandlesCommand<C>
        //{
        //    _bus.RegisterCommandHandler<C, H>();
        //}

        //public void RegisterEventHandler<E, H>()
        //    where E : Event
        //    where H : HandlesEvent<E>
        //{
        //    _bus.RegisterEventHandler<E, H>();
        //}
        //public static void RegisterHandlerForCommand<C, H>()
        //    where C : Command
        //    where H : HandlesCommand<C>
        //{
        //    ApplicationBus.Bus.instance.RegisterCommandHandler<C, H>();


        //}

        //public static void RegisterHandlerForEvent<E, H>()
        //    where E : Event
        //    where H : HandlesEvent<E>
        //{
        //    ApplicationBus.Bus.instance.RegisterEventHandler<E, H>();
        //}
        //#endregion
    }
}
