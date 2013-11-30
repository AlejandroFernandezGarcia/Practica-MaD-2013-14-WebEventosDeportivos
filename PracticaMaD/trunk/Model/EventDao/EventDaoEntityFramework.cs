using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Data.Objects;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventDao
{
    /// <summary>
    /// The DAO implementation of the entity Event.
    /// </summary>
    internal class EventDaoEntityFramework : GenericDaoEntityFramework<Event, Int64>, IEventDao
    {
        /// <summary>
        /// Finds the by keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        public List<Event> FindByKeywords(String keywords, long categoryId)
        {
            String query = "SELECT VALUE e FROM PracticaMaDEntities.Event AS e " +
                           "WHERE e.name LIKE '%' + @keywords + '%' ";
            if (categoryId != -1)
            {
                query += "AND e.categoryId = @categoryId ";
            }

            query += "ORDER BY e.date DESC";

            ObjectParameter param = new ObjectParameter("keywords", keywords);

            List<Event> result;

            if (categoryId != -1)
            {
                ObjectParameter param2 = new ObjectParameter("categoryId", categoryId);
                result = this.Context.CreateQuery<Event>(query, param, param2).ToList();
            }
            else
            {
                result = this.Context.CreateQuery<Event>(query, param).ToList();
            }

            return result;
        }

        /// <summary>
        /// Finds the by keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<Event> FindByKeywords(String keywords, long categoryId, int startIndex, int count)
        {
            String query = "SELECT VALUE e FROM PracticaMaDEntities.Event AS e " +
                           "WHERE e.name LIKE '%' + @keywords + '%' ";
            if (categoryId != -1)
            {
                query += "AND e.categoryId = @categoryId ";
            }

            query += "ORDER BY e.date DESC";

            ObjectParameter param = new ObjectParameter("keywords", keywords);

            List<Event> result;

            if (categoryId != -1)
            {
                ObjectParameter param2 = new ObjectParameter("categoryId", categoryId);
                result = this.Context.CreateQuery<Event>(query, param, param2).Skip(startIndex).Take(count).ToList();
            }
            else
            {
                result = this.Context.CreateQuery<Event>(query, param).Skip(startIndex).Take(count).ToList();
            }

            return result;
        }
    }
}