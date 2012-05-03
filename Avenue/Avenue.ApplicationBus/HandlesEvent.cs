using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avenue.ApplicationBus
{
    public interface HandlesEvent<T> where T : Event
    {
        void Handle(T message);
    }

}
