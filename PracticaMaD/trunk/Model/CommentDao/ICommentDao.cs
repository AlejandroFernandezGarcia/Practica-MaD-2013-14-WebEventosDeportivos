using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    /// <summary>
    /// The DAO of the entity Comment.
    /// </summary>
    public interface ICommentDao : IGenericDao<Comment, Int64>
    {
        /// <summary>
        /// Finds the by event identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<Comment> FindByEventId(long id, int startIndex, int count);

        /// <summary>
        /// Finds the by event identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        List<Comment> FindByEventId(long id);
    }
}