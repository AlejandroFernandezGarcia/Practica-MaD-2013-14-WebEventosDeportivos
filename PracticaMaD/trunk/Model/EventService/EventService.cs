using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventService
{
    class EventService : IEventService
    {
        [Dependency]
        public IEventDao EventDao { private get; set; }

        [Dependency]
        public ICommentDao CommentDao { private get; set; }

        public List<Event> FindByKeywords(string keywords, Category category)
        {
            if (category != null)
            {
                return EventDao.FindByKeywords(keywords, category.id);
            }
            else 
            {
                return EventDao.FindByKeywords(keywords, -1);
            }

        }

        public void AddComment(Event even, string text, UserProfile userProfile)
        {
            DateTime date = new DateTime();
            byte[] dateBytes = BitConverter.GetBytes(date.Ticks);

            Comment comment = Comment.CreateComment(0,dateBytes,text,even.id,userProfile.id);

            CommentDao.Create(comment);
        }
    }
}
