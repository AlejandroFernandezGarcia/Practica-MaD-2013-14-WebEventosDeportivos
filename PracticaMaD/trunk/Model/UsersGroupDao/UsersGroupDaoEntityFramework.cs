using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao
{
    class UsersGroupDaoEntityFramework : GenericDaoEntityFramework<UsersGroup, Int64>, IUsersGroupDao
    {
        public List<UsersGroup> FindByUserId(UserProfile userProfile, int startIndex, int count)
        {
            return userProfile.UsersGroup.Skip(startIndex).Take(count).ToList();
        }

        public int GetNumberOfUserGroups(UserProfile userProfile)
        {
            return userProfile.UsersGroup.Count();
        }


        public List<UsersGroup> FindAllGroups()
        {
            String query = "SELECT VALUE v FROM PracticaMaDEntities.UsersGroup AS v";

            return this.Context.CreateQuery<UsersGroup>(query).ToList();
        }

        public List<UsersGroup> FindByUserId(UserProfile userProfile)
        {
            return userProfile.UsersGroup.ToList();
        }


        public int GetNumberOfUsersForGroup(long usersGroupId)
        {
            UsersGroup usersGroup = Find(usersGroupId);

            return usersGroup.UserProfile.Count();
        }

        public int GetNumberOfRecommendationsForGroup(long usersGroupId)
        {
            UsersGroup usersGroup = Find(usersGroupId);

            return usersGroup.Recommendation.Count();
        }

        public bool IsUsersBelongGroup(UsersGroup usersGroups, UserProfile userProfile)
        {
            return usersGroups.UserProfile.ToList().Contains(userProfile);
        }
    }
}
