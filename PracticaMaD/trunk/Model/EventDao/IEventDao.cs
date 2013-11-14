using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventDao
{
    public interface IEventDao : IGenericDao<Event, Int64>
    {
        [Transactional]
        List<Event> FindByKeywords(String keywords, long categoryId);
    }
}
