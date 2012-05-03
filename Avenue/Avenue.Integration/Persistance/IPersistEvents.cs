using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avenue.Integration.Persistance
{
    interface IPersistEvents
    {
        IEnumerable<string> GetEventsRelatedToMessage(Guid messageId, int skip, int take);
    }
}
