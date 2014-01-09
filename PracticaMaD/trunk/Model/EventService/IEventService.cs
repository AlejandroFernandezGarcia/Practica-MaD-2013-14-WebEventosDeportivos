using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventService
{
    /// <summary>
    /// The interface of the event service.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Sets the event DAO.
        /// </summary>
        /// <value>
        /// The event DAO.
        /// </value>
        [Dependency]
        IEventDao EventDao { set; }

        /// <summary>
        /// Sets the tag service.
        /// </summary>
        /// <value>
        /// The tag service.
        /// </value>
        [Dependency]
        ITagService TagService { set; }

        /// <summary>
        /// Sets the comment DAO.
        /// </summary>
        /// <value>
        /// The comment DAO.
        /// </value>
        [Dependency]
        ICommentDao CommentDao { set; }

        /// <summary>
        /// Sets the category DAO.
        /// </summary>
        /// <value>
        /// The category DAO.
        /// </value>
        [Dependency]
        ICategoryDao CategoryDao { set; }

        /// <summary>
        /// Finds the by keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<EventCategoryDto> FindByKeywords(String keywords, long categoryId, int startIndex, int count);

        /// <summary>
        /// Finds the by keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<EventCategoryDto> FindByKeywords(String keywords, int startIndex, int count);

        /// <summary>
        /// Finds the by keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        List<EventCategoryDto> FindByKeywords(String keywords, long categoryId);

        /// <summary>
        /// Finds the by keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        List<EventCategoryDto> FindByKeywords(String keywords);


        /// <summary>
        /// Adds the comment.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="text">The text.</param>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="tags">The tags.</param>
        [Transactional]
        void AddComment(long eventId, String text, long userProfileId, List<string> tags);

        /// <summary>
        /// Removes the comment.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="commentId">The comment identifier.</param>
        [Transactional]
        void RemoveComment(long eventId, long commentId);

        /// <summary>
        /// Updates the comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="text">The text.</param>
        /// <param name="tags">The tags.</param>
        [Transactional]
        void UpdateComment(long commentId, String text, List<string> tags);

        /// <summary>
        /// Finds all categories.
        /// </summary>
        /// <returns></returns>
        List<Category> FindAllCategories();

        /// <summary>
        /// Finds the comments for event.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns></returns>
        List<Comment> FindCommentsForEvent(long eventId);

        /// <summary>
        /// Finds the comments for event.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<Comment> FindCommentsForEvent(long eventId, int startIndex, int count);

        /// <summary>
        /// Finds the event by identifier.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns></returns>
        Event FindEventById(long eventId);

        /// <summary>
        /// Finds the comment by identifier.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        Comment FindCommentById(long commentId);
        //Todo faltan test de los dos ultimos
    }
}