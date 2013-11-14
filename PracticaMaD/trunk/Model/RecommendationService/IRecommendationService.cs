using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationService
{
    public interface IRecommendationService
    {
        void Create(Event even, UsersGroup usersGroup, String text);
    }
}
