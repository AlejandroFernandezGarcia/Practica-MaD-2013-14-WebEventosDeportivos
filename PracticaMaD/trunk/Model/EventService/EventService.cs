﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;


//TODO Arreglar findByKeywords
namespace Es.Udc.DotNet.PracticaMaD.Model.EventService
{
    /// <summary>
    /// The implementation of the IEventService.
    /// </summary>
    internal class EventService : IEventService
    {
        /// <summary>
        /// Gets or sets the event DAO.
        /// </summary>
        /// <value>
        /// The event DAO.
        /// </value>
        [Dependency]
        public IEventDao EventDao { private get; set; }

        /// <summary>
        /// Gets or sets the comment DAO.
        /// </summary>
        /// <value>
        /// The comment DAO.
        /// </value>
        [Dependency]
        public ICommentDao CommentDao { private get; set; }

        /// <summary>
        /// Gets or sets the category DAO.
        /// </summary>
        /// <value>
        /// The category DAO.
        /// </value>
        [Dependency]
        public ICategoryDao CategoryDao { private get; set; }

        /// <summary>
        /// Sets the tag service.
        /// </summary>
        /// <value>
        /// The tag service.
        /// </value>
        [Dependency]
        public ITagService TagService { private get; set; }
        /// <summary>
        /// Finds the by keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        public List<EventCategoryDto> FindByKeywords(string keywords, long categoryId)
        {
            List<Event> listEvent = EventDao.FindByKeywords(keywords, categoryId);

            List<EventCategoryDto> result = new List<EventCategoryDto>();

            foreach (Event e in listEvent)
            {
                result.Add(new EventCategoryDto(e, CategoryDao.Find(e.categoryId)));
            }

            return result;
        }

        /// <summary>
        /// Finds the by keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        public List<EventCategoryDto> FindByKeywords(string keywords)
        {
            List<Event> listEvent = EventDao.FindByKeywords(keywords, -1);

            List<EventCategoryDto> result = new List<EventCategoryDto>();

            foreach (Event e in listEvent)
            {
                result.Add(new EventCategoryDto(e, CategoryDao.Find(e.categoryId)));
            }

            return result;
        }

        /// <summary>
        /// Adds the comment.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="text">The text.</param>
        /// <param name="userProfileId">The user profile identifier.</param>
        /// <param name="tags">The tags.</param>
        public void AddComment(long eventId, string text, long userProfileId, List<string> tags)
        {

            Comment comment = Comment.CreateComment(0, DateTime.Now, text, eventId, userProfileId);

            CommentDao.Create(comment);

            TagService.AddTagsToComment(tags, comment.id);
        }
        /// <summary>
        /// Removes the comment.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="commentId">The comment identifier.</param>
        public void RemoveComment(long eventId, long commentId)
        {
            Event e = EventDao.Find(eventId);
            Comment c = CommentDao.Find(commentId);

            e.Comment.Load();
            e.Comment.Remove(c);

            CommentDao.Remove(commentId);
        }

        /// <summary>
        /// Updates the comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="text">The text.</param>
        /// <param name="tags">The tags.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void UpdateComment(long commentId, string text, List<string> tags)
        {
            Comment c = CommentDao.Find(commentId);

            c.Tag.Load();
            c.text = text;

            List<string> tagS = new List<string>();
            foreach (Tag t in c.Tag)
            {
                tags.Add(t.tagName);
            }

            TagService.RemoveTagsFromComment(tagS,c.id);

            TagService.AddTagsToComment(tags, c.id);

            CommentDao.Update(c);

        }

        /// <summary>
        /// Finds all categories.
        /// </summary>
        /// <returns></returns>
        public List<Category> FindAllCategories()
        {
            return CategoryDao.FindAllCategories();
        }

        /// <summary>
        /// Finds the comments for event.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<Comment> FindCommentsForEvent(long eventId, int startIndex, int count)
        {
            return CommentDao.FindByEventId(eventId, startIndex, count);
        }

        /// <summary>
        /// Finds the by keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<EventCategoryDto> FindByKeywords(string keywords, long categoryId, int startIndex, int count)
        {
            List<Event> listEvent = EventDao.FindByKeywords(keywords, categoryId, startIndex, count);

            List<EventCategoryDto> result = new List<EventCategoryDto>();

            foreach (Event e in listEvent)
            {
                result.Add(new EventCategoryDto(e, CategoryDao.Find(e.categoryId)));
            }

            return result;
        }

        /// <summary>
        /// Finds the by keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<EventCategoryDto> FindByKeywords(string keywords, int startIndex, int count)
        {
            List<Event> listEvent = EventDao.FindByKeywords(keywords, -1, startIndex, count);

            List<EventCategoryDto> result = new List<EventCategoryDto>();

            foreach (Event e in listEvent)
            {
                result.Add(new EventCategoryDto(e, CategoryDao.Find(e.categoryId)));
            }

            return result;
        }

        /// <summary>
        /// Finds the comments for event.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns></returns>
        public List<Comment> FindCommentsForEvent(long eventId)
        {
            Event e = EventDao.Find(eventId);
            e.Comment.Load();
            List<Comment> result = e.Comment.ToList();
            return result;
        }


        /// <summary>
        /// Finds the event by identifier.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns></returns>
        public Event FindEventById(long eventId)
        {
            return EventDao.Find(eventId);
        }

        /// <summary>
        /// Finds the comment by identifier.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public Comment FindCommentById(long commentId)
        {
            return CommentDao.Find(commentId);
        }
    }
}