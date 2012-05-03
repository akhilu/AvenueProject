using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avenue.ApplicationBus
{
    public interface HandlesCommand<T> where T : Command
    {
        void Handle(T message);
    }
}
