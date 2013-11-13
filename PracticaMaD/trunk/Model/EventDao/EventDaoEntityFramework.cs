using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Data.Objects;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventDao
{
    class EventDaoEntityFramework : GenericDaoEntityFramework<Event, Int64>, IEventDao
    {
        public List<Event> FindByKeywords(String keywords, int categoryId)
        {
            String query = "SELECT VALUE e FROM PracticaMaDEntities.Event AS e " +
                           "WHERE e.name.Contains(@keywords) ";
            if (categoryId != null)
            {
                query += "AND e.categoryId = @categoryId ";
            }

            query += "ORDER BY e.date DESC";

            ObjectParameter param = new ObjectParameter("keywords", keywords);

            List<Event> result;

            if (categoryId != null)
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
    }
}
