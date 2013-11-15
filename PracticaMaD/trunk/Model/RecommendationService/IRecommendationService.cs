using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationService
{
    public interface IRecommendationService
    {
        [Dependency]
        IRecommendationDao RecommendationDao { set; }

        void Create(Event even, UsersGroup usersGroup, String text);
    }
}
