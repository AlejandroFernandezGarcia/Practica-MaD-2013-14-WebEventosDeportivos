using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Es.Udc.DotNet.ModelUtil.Dao;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao
{
    public interface IRecommendationDao : IGenericDao<Recommendation, Int64>
    {

        List<Recommendation> FindRecommendationsForEvent(Event evento);

        List<Recommendation> FindRecommendationsForEvent(Event evento, int startIndex, int count);

    }
}
