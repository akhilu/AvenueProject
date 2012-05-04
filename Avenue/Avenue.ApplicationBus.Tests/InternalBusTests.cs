using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FakeItEasy;

namespace Avenue.ApplicationBus.Tests
{
    [TestFixture]
    public class InternalBusTests
    {
        private InternalBus bus;

        #region Register Handlers
        [Test]
        public void RegisterCommandHandler_ShouldReturnException_WhenRegisteringMultipleCommandHandlersForCommand()
        {
            bus = new InternalBus();
            bus.ResetRoutes();
            bus.RegisterCommandHandler<CreateUserCommand, CreateUserCommandHandler>();
            //  Bus.Instance.RegisterHandler<CreateUserCommand>(new CreateUserCommandHandler().Handle);
            Assert.Throws(typeof(InvalidOperationException), new TestDelegate(RegisterSecondCommandHandler));
        }

        void RegisterSecondCommandHandler()
        {
            bus.RegisterCommandHandler<CreateUserCommand, CreateUserCommandHandler>();
        }

        [Test]
        public void RegisterEventHandler_ShouldReturnSuccede_WhenRegisteringMultipleEventHandlersForEvent()
        {
            bus = new InternalBus();
            bus.RegisterEventHandler<UserCreatedEvent, CreateUserEventHandler>();
            bus.RegisterEventHandler<UserCreatedEvent, CreateUserEventHandlerTwo>();

            Assert.IsTrue(true);
        }

        [Test]
        public void RegisterEventHandler_ShouldReturnException_WhenRegisteringSameEventHandlerMultipleTimes()
        {
            bus = new InternalBus();
            bus.RegisterEventHandler<UserCreatedEvent, CreateUserEventHandler>();
            Assert.Throws(typeof(InvalidOperationException), new TestDelegate(RegisterSecondEventHandler));
        }

        void RegisterSecondEventHandler()
        {
            bus.RegisterEventHandler<UserCreatedEvent, CreateUserEventHandler>();
        }
        #endregion

        #region Publish Event

        #endregion

        #region Send Command

        [Test]
        public void bla()
        {
            bus = new InternalBus();
            bus.ResetRoutes();
            bus.RegisterCommandHandler<CreateUserCommand, CreateUserCommandHandler>();

            var command = new CreateUserCommand() {UserName = "Bob"};
            CreateUserCommandHandler.Called = false;
            bus.Send(command);
            Assert.IsTrue(CreateUserCommandHandler.Called);

        }
       
        #endregion

    }


    #region helper classes

    public class CreateUserCommand : Command
    {
        public string UserName { get; set; }
    }

    public class UserCreatedEvent : Event
    {
        public string UserName { get; set; }
    }

    public class CreateUserCommandHandler : HandlesCommand<CreateUserCommand>
    {
        public void Handle(CreateUserCommand message)
        {
            Called = true;
            Bus.Instance.Publish(new UserCreatedEvent() { UserName = message.UserName });
        }

        public static bool Called { get; set; }
    }

    public class CreateUserEventHandler : HandlesEvent<UserCreatedEvent>
    {
        public void Handle(UserCreatedEvent message)
        {
            Called = true;
        }

        public static bool Called { get; set; }
    }


    public class CreateUserEventHandlerTwo : HandlesEvent<UserCreatedEvent>
    {
        public void Handle(UserCreatedEvent message)
        {
            Called = true;
        }

        public static bool Called { get; set; }
    }

    #endregion
}