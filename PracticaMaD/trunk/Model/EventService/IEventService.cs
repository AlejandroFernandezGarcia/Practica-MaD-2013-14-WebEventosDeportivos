using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventService
{
    public interface IEventService
    {
        [Dependency]
        IEventDao EventDao { set; }

        [Dependency]
        ICommentDao CommentDao { set; }

        List<Event> FindByKeywords(String keywords, Category category);

        void AddComment(Event even, String text, UserProfile userProfile);
    }
}
