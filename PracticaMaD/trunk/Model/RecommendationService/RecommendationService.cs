using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationService
{
    class RecommendationService : IRecommendationService
    {
        [Dependency]
        public IRecommendationDao RecommendationDao { private get; set; }

        public long Create(Event even, UsersGroup usersGroup, string text)
        {
            Recommendation recommendation = Recommendation.CreateRecommendation(0, text, even.id, usersGroup.id);

            RecommendationDao.Create(recommendation);

            return recommendation.id;
        }
    }
}
