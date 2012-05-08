using System;
using Avenue.ApplicationBus;

namespace Avenue.Integration.Tests
{
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

    public class TimeMessageHandler : HandlesCommand<TimeMessage>
    {
        public void Handle(TimeMessage message)
        {
            Console.WriteLine("here");
            Called = true;
        }

        public static bool Called { get; set; }

    }

    #endregion
}
