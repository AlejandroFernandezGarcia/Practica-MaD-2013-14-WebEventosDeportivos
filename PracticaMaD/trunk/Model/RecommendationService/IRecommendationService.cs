using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationService
{
    public interface IRecommendationService
    {
        [Dependency]
        IRecommendationDao RecommendationDao { set; }

        [Dependency]
        public IUsersGroupDao UsersGroupDao { private get; set; }
        
        long Create(long eventId, List<long> usersGroupIds, String text);

        List<Recommendation> FindRecommendationsForEvent(long eventId);

    }
}
