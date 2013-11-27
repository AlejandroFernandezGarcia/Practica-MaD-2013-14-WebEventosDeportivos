using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventService
{
    public interface IEventService
    {
        [Dependency]
        IEventDao EventDao { set; }

        [Dependency]
        ICommentDao CommentDao { set; }

        [Dependency]
        ICategoryDao CategoryDao { set; }

        List<EventCategoryDto> FindByKeywords(String keywords, long categoryId, int startIndex, int count);

        List<EventCategoryDto> FindByKeywords(String keywords, int startIndex, int count);

        List<EventCategoryDto> FindByKeywords(String keywords, long categoryId);

        List<EventCategoryDto> FindByKeywords(String keywords);


        [Transactional]
        void AddComment(long eventId, String text, long userProfileId);

        List<Category> FindAllCategories();

        List<Comment> FindCommentsForEvent(long eventId);

        List<Comment> FindCommentsForEvent(long eventId, int startIndex, int count);
    }
}
