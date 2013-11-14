using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Data.Objects;

namespace Es.Udc.DotNet.PracticaMaD.Model.UsersGroupDao
{
    class UsersGroupEntityFramework : GenericDaoEntityFramework<UsersGroup, Int64>, IUsersGroupDao
    {
        public List<UsersGroup> FindByUserId(long userId, int startIndex, int count)
        {
            String query = "SELECT VALUE e FROM PracticaMaDEntities.UserProfileUsersGroup AS e " +
                           "WHERE e.userId = @userId " +
                            "ORDER BY e.date DESC";

            ObjectParameter param = new ObjectParameter("userId", userId);

            List<UsersGroup> result = this.Context.CreateQuery<UsersGroup>(query, param)
                                            .Skip(startIndex).Take(count).ToList();

            return result;
        }

        public int GetNumberOfUserGroups(long userId)
        {
            String query = "SELECT VALUE e FROM PracticaMaDEntities.UserProfileUsersGroup AS e " +
                            "WHERE e.userId = @userId";

            ObjectParameter param = new ObjectParameter("userId", userId);

            return this.Context.CreateQuery<UsersGroup>(query, param).Count();
        }

    }
}
