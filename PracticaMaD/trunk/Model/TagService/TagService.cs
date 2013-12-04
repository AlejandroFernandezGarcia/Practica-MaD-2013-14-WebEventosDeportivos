using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagService
{
    /// <summary>
    /// The implementation of the Tag service.
    /// </summary>
    internal class TagService : ITagService
    {
        /// <summary>
        /// Gets or sets the tag DAO.
        /// </summary>
        /// <value>
        /// The tag DAO.
        /// </value>
        [Dependency]
        public ITagDao TagDao { private get; set; }

        /// <summary>
        /// Gets or sets the comment DAO.
        /// </summary>
        /// <value>
        /// The comment DAO.
        /// </value>
        [Dependency]
        public ICommentDao CommentDao { private get; set; }

        /// <summary>
        /// Adds the tags to comment.
        /// </summary>
        /// <param name="listOfTags">The list of tags.</param>
        /// <param name="commentId">The comment identifier.</param>
        [Transactional()]
        public void AddTagsToComment(List<string> listOfTags, long commentId)
        {
            List<Tag> listOfObjectTags = new List<Tag>();

            foreach (String tagName in listOfTags)
            {
                try
                {
                    Tag tag = TagDao.FindByTagName(tagName);
                    listOfObjectTags.Add(tag);
                }
                catch (InstanceNotFoundException e)
                {
                    TagDao.Create(Tag.CreateTag(0,tagName));
                    Tag tag = TagDao.FindByTagName(tagName);
                    listOfObjectTags.Add(tag);
                }
            }

            Comment comment = CommentDao.Find(commentId);

            foreach (Tag tagObj in listOfObjectTags)
            {
                if (!comment.Tag.Contains(tagObj))
                {
                    comment.Tag.Add(tagObj);
                    tagObj.Comment.Add(comment);

                    CommentDao.Update(comment);
                    TagDao.Update(tagObj);
                }
                //DUDA Lanzar excepcion?
                //DUDA Hay que actualizar cada vez el objeto o vale actualizarlo una vez todos este metidos?.
            }
        }

        /// <summary>
        /// Removes the tags from comment.
        /// </summary>
        /// <param name="listOfTags">The list of tags.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <exception cref="Es.Udc.DotNet.ModelUtil.Exceptions.InstanceNotFoundException"></exception>
        [Transactional()]
        public void RemoveTagsFromComment(List<string> listOfTags, long commentId)
        {
            List<Tag> listOfObjectTags = new List<Tag>();

            foreach (String tagName in listOfTags)
            {
                try
                {
                    Tag tag = TagDao.FindByTagName(tagName);
                    listOfObjectTags.Add(tag);
                }
                catch (InstanceNotFoundException e)
                {
                    throw new InstanceNotFoundException(tagName, typeof(Tag).FullName);
                }
            }

            Comment comment = CommentDao.Find(commentId);

            foreach (Tag tagObj in listOfObjectTags)
            {
                if (comment.Tag.Contains(tagObj))
                {
                    comment.Tag.Remove(tagObj);
                    tagObj.Comment.Remove(comment);

                    CommentDao.Update(comment);
                    TagDao.Update(tagObj);
                }
                //DUDA Lanzar excepcion?
                //DUDA Hay que actualizar cada vez el objeto o vale actualizarlo una vez todos este metidos?.
            }
        }

        /// <summary>
        /// Finds the tags percent.
        /// </summary>
        /// <returns></returns>
        public List<TagDto> FindTagsPercent()
        {
            List<Tag> listOfAllTags = TagDao.FindAllTags();
            List<long> numberOfOcurrences = new List<long>();
            //TODO Cambiar float por double
            float ocurrences = 0;


            foreach (Tag t in listOfAllTags)
            {
                numberOfOcurrences.Add(t.Comment.Count);
                ocurrences += t.Comment.Count;
            }

            List<TagDto> result = new List<TagDto>();

            for (int i = 0; i < listOfAllTags.Count; i++)
            {
                Tag t = listOfAllTags[i];
                float number = numberOfOcurrences[i];
                
                result.Add(new TagDto(t, (number / ocurrences) * 100));
            }

            return result;

        }

        /// <summary>
        /// Finds the comments by tag.
        /// </summary>
        /// <param name="tagId">The tag identifier.</param>
        /// <returns></returns>
        public List<Comment> FindCommentsByTag(long tagId)
        {
            Tag tag = TagDao.Find(tagId);

            return tag.Comment.ToList();
        }

        /// <summary>
        /// Finds the comments by tag.
        /// </summary>
        /// <param name="tagId">The tag identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<Comment> FindCommentsByTag(long tagId, int startIndex, int count)
        {
            Tag tag = TagDao.Find(tagId);

            return tag.Comment.Skip(startIndex).Take(count).ToList();
        }

        /// <summary>
        /// Finds the tags of comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public List<Tag> FindTagsOfComment(long commentId)
        {
            Comment comment = CommentDao.Find(commentId);

            return comment.Tag.ToList();
        }

        /// <summary>
        /// Finds the tags of comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<Tag> FindTagsOfComment(long commentId, int startIndex, int count)
        {
            Comment comment = CommentDao.Find(commentId);

            return comment.Tag.Skip(startIndex).Take(count).ToList();
        }
    }
}
