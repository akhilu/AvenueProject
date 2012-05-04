using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Avenue.ApplicationBus.Tests
{
    [TestFixture]
    public class BusTests
    {

        private void ResetHelpers()
        {
            CreateUserEventHandler.Called = false;
            CreateUserEventHandlerTwo.Called = false;
            CreateUserCommandHandler.Called = false;

        }
        [Test]
        public void Bus_ShouldAutoWireCommands_WhenCreated()
        {
            ResetHelpers();
            Bus.SendCommand(new CreateUserCommand() {UserName = "bob"});

            Assert.IsTrue(CreateUserCommandHandler.Called);

        }

        [Test]
        public void Bus_ShouldAutoWireEvents_WhenCreated()
        {
            ResetHelpers();
            Bus.PublishEvent(new UserCreatedEvent() { UserName = "bob" });

            Assert.IsTrue(CreateUserEventHandlerTwo.Called);

        }
    }
}
