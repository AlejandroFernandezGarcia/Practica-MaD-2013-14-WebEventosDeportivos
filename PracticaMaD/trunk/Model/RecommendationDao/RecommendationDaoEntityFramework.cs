using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Objects;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao
{
    class RecommendationDaoEntityFramework : GenericDaoEntityFramework<Recommendation, Int64>, IRecommendationDao
    {
        
    }
}
