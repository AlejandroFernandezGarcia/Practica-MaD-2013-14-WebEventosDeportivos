using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventService
{
    class EventService : IEventService
    {
        public List<Event> FindByKeywords(string keywords, Category category)
        {
            throw new NotImplementedException();
        }

        public void AddComment(Event even, string text, UserProfile userProfile)
        {
            throw new NotImplementedException();
        }
    }
}
