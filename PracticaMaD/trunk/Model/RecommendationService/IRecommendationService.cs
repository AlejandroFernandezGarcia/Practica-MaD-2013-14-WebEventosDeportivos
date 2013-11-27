using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationService
{
    public interface IRecommendationService
    {
        [Dependency]
        IRecommendationDao RecommendationDao { set; }

        [Dependency]
        IUserProfileDao UserProfileDao { set; }

        [Dependency]
        IUsersGroupDao UsersGroupDao { set; }

        void Create(long eventId, List<long> usersGroupIds, String text, long userProfileId);

        List<Recommendation> FindRecommendationsForEvent(long eventId);

    }
}
