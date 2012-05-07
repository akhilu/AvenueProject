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
        #region supporting
        private InternalBus bus;

        private Command command;

        void sendBusCommand()
        {
            bus.Send(command);
        }
        
        void RegisterSecondEventHandler()
        {
            bus.RegisterEventHandler<UserCreatedEvent, CreateUserEventHandler>();
        }
        
        void RegisterSecondCommandHandler()
        {
            bus.RegisterCommandHandler<CreateUserCommand, CreateUserCommandHandler>();
        }
        #endregion

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

        #endregion

        #region Publish Event

        [Test]
        public void Publish_ShouldCallRegisteredEvent()
        {
            bus = new InternalBus();
            bus.ResetRoutes();
            bus.RegisterEventHandler<UserCreatedEvent, CreateUserEventHandler>();

            var newEvent = new UserCreatedEvent() { UserName = "Bob" };
            CreateUserEventHandler.Called = false;
            bus.Publish(newEvent);
            Assert.IsTrue(CreateUserEventHandler.Called);

        }

        [Test]
        public void Publish_ShouldCallMultipleRegisteredRegisteredEvents()
        {
            bus = new InternalBus();
            bus.ResetRoutes();
            bus.RegisterEventHandler<UserCreatedEvent, CreateUserEventHandler>();
            bus.RegisterEventHandler<UserCreatedEvent, CreateUserEventHandlerTwo>();
            var newEvent = new UserCreatedEvent() { UserName = "Bob" };
            CreateUserEventHandler.Called = false;
            CreateUserEventHandlerTwo.Called = false;
            bus.Publish(newEvent);
            Assert.IsTrue(CreateUserEventHandler.Called && CreateUserEventHandlerTwo.Called);

        }

        #endregion

        #region Send Command

        [Test]
        public void Send_ShouldCallRegisteredCommands()
        {
            bus = new InternalBus();
            bus.ResetRoutes();
            bus.RegisterCommandHandler<CreateUserCommand, CreateUserCommandHandler>();

            var command = new CreateUserCommand() {UserName = "Bob"};
            CreateUserCommandHandler.Called = false;
            bus.Send(command);
            Assert.IsTrue(CreateUserCommandHandler.Called);

        }

        [Test]
        public void Send_ShouldReturnException_WhenCallingUnRegisteredCommands()
        {
            bus = new InternalBus();
            bus.ResetRoutes();

            command = new CreateUserCommand() { UserName = "Bob" };
            CreateUserCommandHandler.Called = false;
            Assert.Throws(typeof(InvalidOperationException), new TestDelegate(sendBusCommand));

        }

        #endregion

        #region ServiceLocator Test
        [Test]
        public void Publish_ShouldUseServiceLocatorWhenRegistered()
        {

            Assert.IsTrue(false);
        }
        #endregion

    }



}