using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventDao
{
    public interface IEventDao : IGenericDao<Event, Int64>
    {
        List<Event> FindByKeywords(String keywords, int categoryId);
    }
}
