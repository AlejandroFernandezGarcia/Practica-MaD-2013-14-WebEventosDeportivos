using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Objects;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;


namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao
{
    class RecommendationDaoEntityFramework : GenericDaoEntityFramework<Recommendation, Int64>, IRecommendationDao
    {

        public List<Recommendation> FindRecommendationsForEvent(Event evento)
        {
            return evento.Recommendation.ToList();
        }

    }
}
