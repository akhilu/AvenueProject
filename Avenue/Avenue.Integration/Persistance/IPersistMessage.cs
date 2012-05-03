using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avenue.Integration.Persistance
{
    public interface IPersistMessage
    {
        //get recent messages

        //Get 

        IEnumerable<ApplicationBus.Message> GetMessagesByLocalId(Guid messageId, int skip, int limit);

        IEnumerable<ApplicationBus.Message> GetMessagesLike(string search, int skip, int limit);

        void SaveMessage(ApplicationBus.Message message);

     //   IEnumerable<string> GetEventsRelatedToMessage(Guid messageId, int skip, int take);

        //Handle commited and uncommited messages

    }
}
