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

        public void RemoveUserFromGroup(List<long> usersGroupIds, long userProfileId)
        {
            throw new NotImplementedException();
        }

        public void AddUserToGroup(List<long> usersGroupIds, long userProfileId)
        {
            throw new NotImplementedException();

        }

        public List<UsersGroup> FindAllGroups()
        {
            String query = "SELECT VALUE v FROM PracticaMaDEntities.UsersGroup AS v";

            return this.Context.CreateQuery<UsersGroup>(query).ToList();
        }

        public List<UsersGroup> FindAllGroupsOfUser(UserProfile userProfile)
        {
            return userProfile.UsersGroup.ToList();
        }
    }
}
