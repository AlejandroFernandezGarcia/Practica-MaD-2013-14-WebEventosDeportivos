using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventService
{
    class EventService : IEventService
    {
        [Dependency]
        public IEventDao EventDao { private get; set; }

        [Dependency]
        public ICommentDao CommentDao { private get; set; }

        [Dependency]
        ICategoryDao CategoryDao { private get; set; }

        public List<EventCategoryDto> FindByKeywords(string keywords, long categoryId)
        {
            List<Event> listEvent = EventDao.FindByKeywords(keywords, categoryId);

            List<EventCategoryDto> result = new List<EventCategoryDto>();

            foreach(Event e in listEvent){
                result.Add(new EventCategoryDto(e,e.Category));
            }

            return result;
        }

        public List<EventCategoryDto> FindByKeywords(string keywords)
        {
            List<Event> listEvent = EventDao.FindByKeywords(keywords, -1);

            List<EventCategoryDto> result = new List<EventCategoryDto>();

            foreach(Event e in listEvent){
                result.Add(new EventCategoryDto(e,e.Category));
            }

            return result;
        }

        public void AddComment(long eventId, string text, long userProfileId)
        {
            DateTime date = new DateTime();
            byte[] dateBytes = BitConverter.GetBytes(date.Ticks);

            Comment comment = Comment.CreateComment(0, dateBytes, text, eventId, userProfileId);

            CommentDao.Create(comment);
        }

        public List<Category> FindAllCategories()
        {
            return CategoryDao.FindAllCategories();
        }

        public List<Comment> FindCommentsForEvent(long eventId, int startIndex, int count)
        {
            return CommentDao.FindByEventId(eventId, startIndex, count);
        }
    }
}
