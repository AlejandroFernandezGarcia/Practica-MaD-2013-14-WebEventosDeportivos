using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileUsersGroupDao
{
    public interface IUserProfileUsersGroupDao : IGenericDao<UserProfileUsersGroup,Int64>
    {
        void RemoveUserFromGroup(long usersGroupId, long userProfileId);

        void AddUserToGroup(UserProfileUsersGroup userProfileUsersGroup);

        UserProfileUsersGroup FindByUserIdAndGroupId(long usersGroupId, long userProfileId);
    }
}
