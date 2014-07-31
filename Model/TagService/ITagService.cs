using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagService
{
    /// <summary>
    /// The interface of the Tag service.
    /// </summary>
    public interface ITagService
    {
        /// <summary>
        /// Sets the tag DAO.
        /// </summary>
        /// <value>
        /// The tag DAO.
        /// </value>
        [Dependency]
        ITagDao TagDao { set; }

        /// <summary>
        /// Sets the comment DAO.
        /// </summary>
        /// <value>
        /// The comment DAO.
        /// </value>
        [Dependency]
        ICommentDao CommentDao { set; }

        /// <summary>
        /// Adds the tags to comment.
        /// </summary>
        /// <param name="listOfTags">The list of tags.</param>
        /// <param name="commentId">The comment identifier.</param>
        void AddTagsToComment(List<string> listOfTags, long commentId);

        /// <summary>
        /// Removes the tags from comment.
        /// </summary>
        /// <param name="listOfTags">The list of tags.</param>
        /// <param name="commentId">The comment identifier.</param>
        void RemoveTagsFromComment(List<string> listOfTags, long commentId);

        /// <summary>
        /// Finds the tags percent.
        /// </summary>
        /// <returns></returns>
        List<TagDto> FindTagsPercent();

        /// <summary>
        /// Finds the comments by tag.
        /// </summary>
        /// <param name="tagId">The tag identifier.</param>
        /// <returns></returns>
        List<Comment> FindCommentsByTag(long tagId);

        /// <summary>
        /// Finds the comments by tag.
        /// </summary>
        /// <param name="tagId">The tag identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<Comment> FindCommentsByTag(long tagId, int startIndex, int count);

        /// <summary>
        /// Finds the tags of comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        List<Tag> FindTagsOfComment(long commentId);

        /// <summary>
        /// Finds the tags of comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<Tag> FindTagsOfComment(long commentId, int startIndex, int count);


        /// <summary>
        /// Finds the tag by identifier.
        /// </summary>
        /// <param name="tagId">The tag identifier.</param>
        /// <returns></returns>
        Tag FindTagById(long tagId);

    }
}
