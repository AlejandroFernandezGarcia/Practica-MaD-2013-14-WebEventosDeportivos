using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventDao
{
    /// <summary>
    /// The DAO interface of the entity Event.
    /// </summary>
    public interface IEventDao : IGenericDao<Event, Int64>
    {
        /// <summary>
        /// Finds the by keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        List<Event> FindByKeywords(String keywords, long categoryId);

        /// <summary>
        /// Finds the by keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        List<Event> FindByKeywords(String keywords, long categoryId, int startIndex, int count);
    }
}