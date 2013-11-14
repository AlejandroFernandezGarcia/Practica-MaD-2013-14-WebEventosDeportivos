using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventService
{
    public interface IEventService
    {
        List<Event> FindByKeywords(String keywords, Category category);

        void AddComment(Event even, String text, UserProfile userProfile);
    }
}
